using System.Threading.Tasks;
using YamhilliaNET.Models;
using YamhilliaNET.Models.Entities;
using YamhilliaNET.Models.User;

namespace YamhilliaNET.Services.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Authenticate a user given their username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>The user if authorized, or null if not</returns>
        Task<User> Authenticate(string username, string password);

        /// <summary>
        /// Create a new user with the given data.
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        Task<User> CreateUser(CreateUser createUser);


        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        Task<User> UpdateUser(UpdateUser updateUser);

        /// <summary>
        /// Get a user given their username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The user if they exist or null if not</returns>
        Task<User> GetUserByUsername(string username);

        /// <summary>
        /// Get a user given their database id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The user if they exist or null if not</returns>
        Task<User> GetUserById(long id);
        
    }
}