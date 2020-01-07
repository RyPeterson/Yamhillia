using System.Collections.Generic;
using System.Threading.Tasks;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services
{
    public interface IFarmService : CRUDService<Farm>
    {
        Task<IEnumerable<YamhilliaUser>> GetMembers(long farmId);

        Task<IEnumerable<Animal>> GetAnimals(long farmId);
    }
}