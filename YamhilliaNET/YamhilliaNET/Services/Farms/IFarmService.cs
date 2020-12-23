using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using YamhilliaNET.Constants;
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


        /// <summary>
        /// Add a user to the farm with the given level.
        /// If the user + level exists, then nothing happens
        /// If the user exists at a different level and is not an OWNER, they will be changed to that level
        /// If the user doesn't exist in the farm, the membership will be added.
        /// OWNER can only add new owners or administrators
        /// OWNER and ADMINISTRATORS can add WORKERS and GUESTs
        /// </summary>
        /// <param name="addToFarmParams">The request data</param>
        /// <returns>The new list of farm memberships for the user</returns>
        Task<List<FarmMembership>> AddUserToFarm(AddUserToFarmParams addToFarmParams);

        /// <summary>
        /// Return 0 or more farm memberships for the farm.
        /// Empty if there are no memberships or if the farm is not found.
        /// </summary>
        /// <param name="farmId">the id of the farm to get the members of</param>
        /// <returns>a collection of farm members (eagerly loaded)</returns>
        Task<List<FarmMembership>> GetFarmMembers(long farmId);
    }
}