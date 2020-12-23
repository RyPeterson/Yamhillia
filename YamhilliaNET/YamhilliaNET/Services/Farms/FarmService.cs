using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YamhilliaNET.Constants;
using YamhilliaNET.Data;
using YamhilliaNET.Exceptions;
using YamhilliaNET.Models.Entities;
using YamhilliaNET.Models.Farms;
using YamhilliaNET.Services.Users;
using YamhilliaNET.Util.Preconditions;

namespace YamhilliaNET.Services.Farms
{
    public class FarmService: IFarmService
    {
        private readonly YamhilliaContext _db;
        private readonly IUserService _userService;
        private readonly ILogger<FarmService> _logger;

        public FarmService(YamhilliaContext db, IUserService userService, ILogger<FarmService> logger)
        {
            _userService = userService;
            _logger = logger;
            _db = db;
        }

        public async Task<Farm> CreateFarm(long ownerId, CreateFarmParams createFarmParams)
        {
            if (createFarmParams == null)
            {
                throw new YamhilliaBadRequestError("params must be provided");
            }
            
            if (string.IsNullOrWhiteSpace(createFarmParams.Name))
            {
                throw new YamhilliaBadRequestError("Name is required");
            }
            
            var existing = await GetFarmByOwner(ownerId);
            if (existing !=  null)
            {
                throw new YamhilliaBadRequestError("User already has created a farm");
            }
            
            var owner = ObjectPreconditions.ExistsOrNotFound(await _userService.GetUserById(ownerId));
            var entity = _db.Farms.Add(new Farm {
                Name = createFarmParams.Name, 
                // WTF/Minute: using just an array causes adding new members to fail (at least in tests)
                FarmMembers = new List<FarmMembership>
            {
                new FarmMembership
                {
                    UserId = ownerId, 
                    MemberType = MemberType.OWNER
                }
            }});
            await _db.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Farm> GetFarmByOwner(long ownerId)
        {
            /*
             * select first(*) from farm_memberships where userId = ownerId and memberType = 'OWNER'
             */
            var existing = await _db.FarmMemberships
                .Where(m => m.UserId == ownerId && m.MemberType == MemberType.OWNER)
                .Include(m => m.Farm)
                .FirstOrDefaultAsync();
            return existing?.Farm;
        }

        public Task<List<FarmMembership>> GetUserMemberships(long userId)
        {
            return _db.FarmMemberships.Where(m => m.UserId == userId).ToListAsync();
        }

        public async Task<Farm> GetFarmById(long farmId)
        {
            return await _db.Farms.FindAsync(farmId);
        }

        public async Task<List<FarmMembership>> AddUserToFarm(AddUserToFarmParams addToFarmParams)
        {
            var farm = await GetFarmById(addToFarmParams.FarmId);
            if (farm == null)
            {
                throw new YamhilliaNotFoundError("Farm not found.");
            }
            var userMemberships = await GetUserMemberships(addToFarmParams.UserId);
            var existingFarmMemberships = userMemberships.Where(m => m.UserId == addToFarmParams.UserId).ToList();

            // This membership already exists
            if (
                existingFarmMemberships.Find(m => m.MemberType == addToFarmParams.MemberType) !=  null)
            {
                _logger.LogInformation("Found existing farm membership.");
                return userMemberships;
            }
            
            AssertUserCanAddMember(addToFarmParams.FarmId, await GetUserMemberships(addToFarmParams.RequesterId), addToFarmParams.MemberType);

            FarmMembership membership = null;
            bool edit = false;
            // If there is an existing membership in this farm, edit it
            if (existingFarmMemberships.Count > 1)
            {
                membership = existingFarmMemberships[0];
                _logger.LogInformation($"Editing existing membership {membership.Id}, {membership.MemberType} => {addToFarmParams.MemberType}");
                membership.MemberType = addToFarmParams.MemberType;
                edit = true;
            }
            else
            {
                // Otherwise creating a new record.
                membership = new FarmMembership
                {
                    UserId = addToFarmParams.UserId,
                    FarmId = addToFarmParams.FarmId,
                    MemberType = addToFarmParams.MemberType
                };
            }


            if (edit)
            {
                _db.FarmMemberships.Update(membership);
            }
            else
            {
                _db.FarmMemberships.Add(membership);
            }

            await _db.SaveChangesAsync();

            return await GetUserMemberships(addToFarmParams.UserId);
        }

        private void AssertUserCanAddMember(long farmId, List<FarmMembership> requesterMemberships, MemberType typeBeingAdded)
        {
            // Get the requester's memberships in the farm being added to
            var currentFarmMemberships = requesterMemberships.Where(m => m.FarmId == farmId).ToList();

            if (currentFarmMemberships.Count < 1)
            {
                throw new YamhilliaBadRequestError("User cannot add users to this farm because they are not a member of it.");
            }
            switch (typeBeingAdded)
            {
                // Only owners are allowed to create owners and admins
                case MemberType.OWNER:
                case MemberType.ADMINISTRATOR:
                    if (currentFarmMemberships.Find(m => m.MemberType == MemberType.OWNER) == null)
                    {
                        _logger.LogWarning($"User {requesterMemberships[0].Id} tried adding {typeBeingAdded} to a farm.");
                        throw new YamhilliaBadRequestError("This user type can only be added by an owner");
                    }
                    break;
                // Guests and workers are not allowed to add anyone
                case MemberType.WORKER:
                case MemberType.GUEST:
                    if (currentFarmMemberships.Find(m => m.MemberType == MemberType.OWNER || m.MemberType == MemberType.ADMINISTRATOR) == null)
                    {
                        _logger.LogWarning($"User {requesterMemberships[0].Id} tried adding {typeBeingAdded} to a farm.");
                        throw new YamhilliaBadRequestError("This user type can only be added by a owners or administrators.");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Task<List<FarmMembership>> GetFarmMembers(long farmId)
        {
            return _db.FarmMemberships
                .Where(m => m.FarmId == farmId)
                .Include(m => m.User)
                .ToListAsync();
        }
    }
}