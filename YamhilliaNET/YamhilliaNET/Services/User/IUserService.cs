using System;
using System.Threading.Tasks;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services.User
{
    public interface IUserService
    {
        Task<YamhilliaUser> GetUserByUsernameAndPassword(string username, string password);

        Task<YamhilliaUser> Create(CreateUserModel createUserModel);

        Task<YamhilliaUser> FindByEmail(string email);

        Task<YamhilliaUser> GetUserById(string id);
    }


    public class InvalidUserNameOrPasswordException : Exception 
    {
        public InvalidUserNameOrPasswordException() : base("Username or password is invalid")
        {

        }
    }
}