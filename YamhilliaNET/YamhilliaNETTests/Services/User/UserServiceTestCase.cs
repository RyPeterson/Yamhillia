using System;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Xunit;
using YamhilliaNET.Exceptions;
using YamhilliaNET.Models;
using YamhilliaNET.Services;
using YamhilliaNET.Services.User;

namespace YamhilliaNETTests.Services.User
{
    public class UserServiceTestCase: IntegrationTestCase
    {
        private readonly UserService _userService;
        
        public UserServiceTestCase()
        {
            _userService = new UserService(GetDbContext());
        }

        [Fact]
        public async void Test_CreateUser_Success()
        {
            var userToCreate = new CreateUser()
            {
                Username = $@"TestUser{Guid.NewGuid().ToString()}@test.com",
                Password = UNIVERSAL_USER_PASSWORD,
            };
            var created = await _userService.CreateUser(userToCreate);
            Assert.NotNull(created);
            Assert.NotEqual(0, created.Id);
            Assert.Equal(created.Username, userToCreate.Username);
        }
        
        [Fact]
        public async void Test_CreateUser_InvalidEmail()
        {
            var userToCreate = new CreateUser()
            {
                Username = $@"TestUser{Guid.NewGuid().ToString()}",
                Password = UNIVERSAL_USER_PASSWORD,
            };
            await Assert.ThrowsAsync<YamhilliaBadRequestError>(() => _userService.CreateUser(userToCreate));
        }

        [Fact]
        public async void Test_Authenticate()
        {
            var user = await CreateTestUser();
            var result1 = await _userService.Authenticate(user.Username, UNIVERSAL_USER_PASSWORD);
            Assert.NotNull(result1);
            var result2 = await _userService.Authenticate(user.Username, "nope");
            Assert.Null(result2);
            var result3 = await _userService.Authenticate("nope", UNIVERSAL_USER_PASSWORD);
            Assert.Null(result3);
            var result4 = await _userService.Authenticate("nope", "nope");
            Assert.Null(result4);
        }

        [Fact]
        public async void Test_GetUserById()
        {
            var noUser = await _userService.GetUserById(-1);
            Assert.Null(noUser);

            var user = await CreateTestUser();
            var foundUser = await _userService.GetUserById(user.Id);
            Assert.NotNull(foundUser);
            Assert.Equal(user.Id, foundUser.Id);
        }

        [Fact]
        public async void Test_GetUserByUsername()
        {
            var noUser = await _userService.GetUserByUsername("nope");
            Assert.Null(noUser);
            var user = await CreateTestUser();
            var found = await _userService.GetUserByUsername(user.Username);
            Assert.NotNull(found);
            Assert.Equal(user.Id, found.Id);
        }

        [Fact]
        public async void Test_UpdateUser()
        {
            var user = await CreateTestUser();
            var newUsername1 = $@"{Guid.NewGuid().ToString()}@test.com";
            await _userService.UpdateUser(new UpdateUser()
            {
                Id = user.Id,
                Username = newUsername1
            });
            var updated1 = await _userService.GetUserById(user.Id);
            Assert.Equal(updated1.Username, newUsername1);
            var authentication1 = await _userService.Authenticate(newUsername1, UNIVERSAL_USER_PASSWORD);
            Assert.NotNull(authentication1);
            
            var newPassword = $@"{UNIVERSAL_USER_PASSWORD}123";
            await _userService.UpdateUser(new UpdateUser()
            {
                Id = user.Id,
                Username = newUsername1,
                Password = newPassword
            });
            
            var authentication2 = await _userService.Authenticate(newUsername1, newPassword);
            Assert.NotNull(authentication2);
        }
    }
}