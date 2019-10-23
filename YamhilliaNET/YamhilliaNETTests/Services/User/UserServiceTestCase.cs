using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YamhilliaNET.Services.User;

namespace YamhilliaNETTests.Services.User
{
    public class UserServiceTestCase : IntegrationTestCase
    {
        private readonly UserService service;
        
        public UserServiceTestCase()
        {
            service = (UserService) GetService<IUserService>();
        }

        [Fact]
        public async void TestCreate_Success()
        {
            var user = new CreateUserModel()
            {
                Email = $"{new Guid().ToString()}@test.com",
                FirstName = "Test",
                Password = UNIVERSAL_USER_PASSWORD,
                LastName = "User"
            };  

            var created = await service.Create(user);
            Assert.NotNull(created);
            var dbCheck = await GetApplicationDbContext().Users.Where(u => u.Email == user.Email).FirstAsync();
            Assert.NotNull(dbCheck);
        }

        [Fact]
        public async void TestCreate_ExistingEmail()
        {
            var user = await CreateUser();

            var newUser = new CreateUserModel()
            {
                Email = user.Email,
                FirstName = "Test",
                Password = UNIVERSAL_USER_PASSWORD,
                LastName = "User"
            };  
            await Assert.ThrowsAsync<InvalidUserNameOrPasswordException>(() => service.Create(newUser));
        }

        [Fact]
        public async void TestGetUserByUsernameAndPassword_Success()
        {
            var user = await CreateUser();

            var found = await service.GetUserByUsernameAndPassword(user.Email, UNIVERSAL_USER_PASSWORD);
            Assert.NotNull(found);
        }

        [Fact]
        public async void TestGetUserByUsernameAndPassword_BadPassword()
        {
            var user = await CreateUser();

            await Assert.ThrowsAsync<InvalidUserNameOrPasswordException>(() => service.GetUserByUsernameAndPassword(user.Email, "This is not the password"));
        }

        [Fact]
        public async void TestGetUserByUsernameAndPassword_NoUser()
        {
            await Assert.ThrowsAsync<InvalidUserNameOrPasswordException>(() => service.GetUserByUsernameAndPassword("notfound@test.com", UNIVERSAL_USER_PASSWORD));
        }
    }
}