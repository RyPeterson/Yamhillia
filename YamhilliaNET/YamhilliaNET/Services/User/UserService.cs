using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YamhilliaNET.Constants;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<YamhilliaUser> userManager;
        private readonly IFarmService farmService;

        public UserService(UserManager<YamhilliaUser> userManager, IFarmService farmService)
        {
            this.userManager = userManager;
            this.farmService = farmService;
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
                var farmKey = createUserModel.FarmKey;
                if(string.IsNullOrEmpty(farmKey)) 
                {
                    farmKey = DefaultFarm.DefaultFarmKey;
                }

                var createdUser = await userManager.FindByEmailAsync(createUserModel.Email);
                var farm = await farmService.GetFarmByKey(farmKey);
                if(farm != null)
                {
                    createdUser.FarmId = farm.Id;
                    await userManager.UpdateAsync(createdUser);
                }
                return createdUser;
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