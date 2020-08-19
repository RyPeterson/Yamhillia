using System.Threading.Tasks;
using YamhilliaNET.Models.Entities;
using YamhilliaNET.Models.Farms;

namespace YamhilliaNET.Services.Farms
{
    public interface IFarmService
    {
        /// <summary>
        /// Create a new farm for the user.
        /// Precondition: the user has not created a farm yet.
        /// Postcondition: a farm is created and owned by the farmer
        /// </summary>
        /// <param name="ownerId">the ID of the owner</param>
        /// <param name="createFarmParams">the request to create the farm</param>
        /// <returns>A task that resolves with the newly created farm</returns>
        Task<Farm> CreateFarm(long ownerId, CreateFarmParams createFarmParams);

        /// <summary>
        /// Return a farm created by the user.
        /// Precondition: none
        /// </summary>
        /// <param name="ownerId">the owner of the farm</param>
        /// <returns>a task that resolves with a farm if it exists, or null</returns>
        Task<Farm> GetFarmByOwner(long ownerId);

        /// <summary>
        /// Return a farm given its id. The Farm need not exist
        /// </summary>
        /// <param name="farmId">the id of the farm</param>
        /// <returns>The farm if it exists, or null if not</returns>
        Task<Farm> GetFarmById(long farmId);
        
    }
}