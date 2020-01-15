using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamhilliaNET.Constants;
using YamhilliaNET.Data;
using YamhilliaNET.Models;
using YamhilliaNET.Utils;

namespace YamhilliaNET.Services
{
    public class AnimalService : AbstractCRUDService<Animal>, IAnimalService
    {
        private readonly IFarmService _farmService;
        public AnimalService(ApplicationDbContext dbContext, IFarmService farmService) : base(dbContext, dbContext.Animals)
        {
            _farmService = farmService;
        }

        public async Task<Animal> Create(Animal animal, Farm farm, YamhilliaUser creator)
        {
            if(farm == null)
            {
                YamhilliaExceptions.BadRequest("Farm is required");
            }
            if(creator == null)
            {
                YamhilliaExceptions.BadRequest("User is required");
            }

            if(farm.Id != creator.FarmId)
            {
                YamhilliaExceptions.BadRequest("User cannot create animal in a farm they are not part of");
            }
            animal.Farm = farm;
            animal.CreatedBy = creator;
            return await Create(animal);
        }

        public async Task<IEnumerable<Animal>> GetAccessibleAnimals(YamhilliaUser user)
        {
            var farm = await _farmService.Get(user.FarmId);
            if(farm.Key == DefaultFarm.DefaultFarmKey) 
            {
                return await _table.Where(animal => animal.CreatedById == user.Id).ToListAsync();
            }
            else
            {
                // All animals in the farm
                return await _table.Where(animal => animal.FarmId == user.FarmId).ToListAsync();
            }
        }

        protected override IQueryable<Animal> _Get(GetOptions options, IQueryable<Animal> query)
        {
            return query;
        }
    }
}