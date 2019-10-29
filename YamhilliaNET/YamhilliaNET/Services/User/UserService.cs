using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<YamhilliaUser> userManager;

        public UserService(UserManager<YamhilliaUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<YamhilliaUser> Create(CreateUserModel createUserModel)
        {
            var user = await userManager.FindByEmailAsync(createUserModel.Email);
            if(user != null)
            {
                throw new InvalidUserNameOrPasswordException();
            }

            var yamhilliaUser = new YamhilliaUser()
            {
                UserName = createUserModel.Email,
                Email = createUserModel.Email,
            };
            var result = await userManager.CreateAsync(yamhilliaUser, createUserModel.Password);
            if(result.Succeeded)
            {
                return await userManager.FindByEmailAsync(createUserModel.Email);
            }
            throw new InvalidUserNameOrPasswordException();
        }

        public async Task<YamhilliaUser> FindByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<YamhilliaUser> GetUserById(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<YamhilliaUser> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await userManager.FindByEmailAsync(username);
            if(user == null)
            {
                throw new InvalidUserNameOrPasswordException();
            }

            if(await userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }
            throw new InvalidUserNameOrPasswordException();
        }
    }
}