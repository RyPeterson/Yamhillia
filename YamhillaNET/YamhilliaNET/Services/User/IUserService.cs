using System.Threading.Tasks;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Authenticate a user given their username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>The user if authorized, or null if not</returns>
        Task<Models.Entities.User> Authenticate(string username, string password);

        /// <summary>
        /// Create a new user with the given data.
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        Task<Models.Entities.User> CreateUser(CreateUser createUser);


        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        Task<Models.Entities.User> UpdateUser(UpdateUser updateUser);

        /// <summary>
        /// Get a user given their username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The user if they exist or null if not</returns>
        Task<Models.Entities.User> GetUserByUsername(string username);

        /// <summary>
        /// Get a user given their database id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The user if they exist or null if not</returns>
        Task<Models.Entities.User> GetUserById(long id);
    }
}