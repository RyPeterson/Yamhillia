using System.Threading.Tasks;
using System.Collections.Generic;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services
{

    public interface CRUDService<T> where T: YamhilliaModel
    {
        Task<T> Create(T model);

        Task<T> Get(long id);

        Task<IEnumerable<T>> Get(GetOptions options);

        Task<T> Update(T model);

        Task Delete(T model);

        Task Delete(long id);
    }
}