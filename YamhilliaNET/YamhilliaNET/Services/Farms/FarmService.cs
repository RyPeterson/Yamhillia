using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public FarmService(YamhilliaContext db, IUserService userService)
        {
            _userService = userService;
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
            var entity = _db.Farms.Add(new Farm {Name = createFarmParams.Name, FarmMembers = new []
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

        public async Task<Farm> GetFarmById(long farmId)
        {
            return await _db.Farms.FindAsync(farmId);
        }
    }
}