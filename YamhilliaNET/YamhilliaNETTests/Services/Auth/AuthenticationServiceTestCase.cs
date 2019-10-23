using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Xunit;
using YamhilliaNET.Controllers;
using YamhilliaNET.Services.Auth;
using YamhilliaNET.Services.User;

namespace YamhilliaNETTests.Services.Auth
{
    public class AutenticationServiceTestCase : IntegrationTestCase
    {
        private readonly AuthenticationService service;
        public AutenticationServiceTestCase()
        {
            service = (AuthenticationService) GetService<IAuthenticationService>();
        }

        [Fact]
        public async void TestCreateToken_Succes()
        {
            var user = await CreateUser();
            var token = await service.CreateToken(new LoginModel() {Username = user.Email, Password = UNIVERSAL_USER_PASSWORD});
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public async void TestCreateToken_BadPassword()
        {
            var user = await CreateUser();
            await Assert.ThrowsAsync<InvalidUserNameOrPasswordException>(async () => await service.CreateToken(new LoginModel() {Username = user.Email, Password ="This is not the password"}));
        }

        [Fact]
        public async void TestCreateToken_UserDoesntExist()
        {
            await Assert.ThrowsAsync<InvalidUserNameOrPasswordException>(async () => await service.CreateToken(new LoginModel() {Username = "FOOBAR@test.com", Password ="This is not the password"}));
        }
    }
}