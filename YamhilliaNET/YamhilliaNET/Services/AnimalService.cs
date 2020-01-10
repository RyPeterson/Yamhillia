using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamhilliaNET.Data;
using YamhilliaNET.Models;
using YamhilliaNET.Utils;

namespace YamhilliaNET.Services
{
    public class AnimalService : AbstractCRUDService<Animal>, IAnimalService
    {
        public AnimalService(ApplicationDbContext dbContext) : base(dbContext, dbContext.Animals)
        {
        }

        public async Task<Animal> Create(Animal animal, Farm farm)
        {
            if(farm == null)
            {
                YamhilliaExceptions.BadRequest("Farm is required");
            }
            animal.Farm = farm;
            return await Create(animal);
        }

        protected override IQueryable<Animal> _Get(GetOptions options, IQueryable<Animal> query)
        {
            return query;
        }
    }
}