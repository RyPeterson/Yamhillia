using System.Collections.Generic;
using System.Threading.Tasks;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services
{
    public interface IAnimalService: CRUDService<Animal>
    {
        Task<Animal> Create(Animal animal, Farm farm, YamhilliaUser creator);
        Task<IEnumerable<Animal>> GetAccessibleAnimals(YamhilliaUser user);
    }
}