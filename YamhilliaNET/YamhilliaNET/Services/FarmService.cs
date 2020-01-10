using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YamhilliaNET.Data;
using YamhilliaNET.Models;
using Microsoft.EntityFrameworkCore;
using YamhilliaNET.Utils;

namespace YamhilliaNET.Services
{
    public class FarmService : AbstractCRUDService<Farm>,  IFarmService
    {
        public FarmService(ApplicationDbContext dbContext) : base(dbContext, dbContext.Farms)
        {
        }

        public async Task<IEnumerable<Animal>> GetAnimals(long farmId)
        {
            return await _db.Animals.Where(animal => animal.FarmId == farmId).ToListAsync();
        }

        public async Task<IEnumerable<YamhilliaUser>> GetMembers(long farmId)
        {
            return await _db.Users.Where(user => user.FarmId == farmId).ToListAsync();
        }

        protected override IQueryable<Farm> _Get(GetOptions options, IQueryable<Farm> query)
        {
            return query;
        }

        public override async Task<Farm> Create(Farm model)
        {
            if(string.IsNullOrEmpty(model.Key))
            {
                model.Key = Guid.NewGuid().ToString();
            }
            else
            {
                await AssertUniqueFarmKey(model.Key, null);
            }
            return await base.Create(model);
        }

        public override async Task<Farm> Update(Farm model)
        {

            await AssertUniqueFarmKey(model.Key, model.Id);
            return await base.Update(model);
        }

        private async Task AssertUniqueFarmKey(string key, long? id)
        {
            var farmQuery = _table.Where(farm => farm.Key == key);
            Farm existing = await farmQuery.FirstOrDefaultAsync();
            if(existing != null)
            {
                if(id.HasValue)
                {
                    if(id.Value == existing.Id)
                    {
                        return;
                    }
                }
                throw new InvalidOperationException("Attempted to create farm with existing key");
            }
        }

        public async Task<Farm> GetFarmByKey(string farmKey)
        {
            if(string.IsNullOrEmpty(farmKey))
            {
                return null;
            }

            return await _table.Where(f => f.Key == farmKey).FirstOrDefaultAsync();
        }
    }
}