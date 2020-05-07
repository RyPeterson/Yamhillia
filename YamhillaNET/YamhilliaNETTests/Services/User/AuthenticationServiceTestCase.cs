using System.Threading;
using Microsoft.Extensions.Options;
using Xunit;
using YamhillaNET.Services.User;
using YamhillaNET.Util;

namespace YamhilliaNETTests.Services.User
{
    public class AuthenticationServiceTestCase: IntegrationTestCase
    {
        private readonly AuthenticationService _authenticationService;
        public AuthenticationServiceTestCase()
        {
            _authenticationService = new AuthenticationService(GetService<IUserService>(), new MockOptions());
        }

        [Fact]
        public async void Test_Authenticate_Success()
        {
            var user = await  CreateTestUser();
            var token1 = await _authenticationService.GenerateToken(user.Username, UNIVERSAL_USER_PASSWORD);
            Assert.NotNull(token1);
            // Too fast, might end up with the same time. Sue me
            Thread.Sleep(1000);
            var token2 = await _authenticationService.GenerateToken(user.Username, UNIVERSAL_USER_PASSWORD);
            Thread.Sleep(1000);
            var token3 = await _authenticationService.GenerateToken(user.Username, UNIVERSAL_USER_PASSWORD);
            Thread.Sleep(1000);
            var token4 = await _authenticationService.GenerateToken(user.Username, UNIVERSAL_USER_PASSWORD);
            
            Assert.NotEqual(token1, token2);
            Assert.NotEqual(token1, token3);
            Assert.NotEqual(token1, token4);
        }
        
        private class MockOptions : IOptions<AppSettings>
        {
            private readonly AppSettings TestSettings = new AppSettings()
            {
                Secret = "thisisatestsecretlollol"
            };

            public AppSettings Value
            {
                get
                {
                    return TestSettings;
                }
            }
        }


    }
}