using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamhilliaNET.Data;
using YamhilliaNET.Exceptions;
using YamhilliaNET.Models;
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
            var entity = await _db.Farms.AddAsync(new Farm() {Name = createFarmParams.Name, OwnerId = owner.Id});
            await _db.SaveChangesAsync();
            return entity.Entity;
        }

        public Task<Farm> GetFarmByOwner(long ownerId)
        {
            return _db.Farms
                .Where(f => f.OwnerId == ownerId)
                .Include(f => f.Owner)
                .FirstOrDefaultAsync();
        }

        public async Task<Farm> GetFarmById(long farmId)
        {
            return await _db.Farms.FindAsync(farmId);
        }
    }
}