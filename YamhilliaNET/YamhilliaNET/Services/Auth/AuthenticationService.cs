using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YamhilliaNET.Models;
using YamhilliaNET.Services.User;

namespace YamhilliaNET.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private static readonly int EXPIRATION_DAYS = 3;

        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        public AuthenticationService(IConfiguration config, IUserService userService)
        {
            this.configuration = config;
            this.userService = userService;
        }

        public async Task<string> CreateToken(LoginModel loginModel)
        {
            var userAndToken = await Login(loginModel);
            return userAndToken.Token;
        }

        public async Task<UserAndToken> Login(LoginModel loginModel)
        {
            var user = await userService.GetUserByUsernameAndPassword(loginModel.Username, loginModel.Password);

            return Login(user);
        }

        public UserAndToken Login(YamhilliaUser user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // PedRelax: server is running therefore the key exists
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["JWT:Issuer"], 
                configuration["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(EXPIRATION_DAYS),
                signingCredentials: creds
            );

            user.PasswordHash = null;

            return new UserAndToken()
            {
                User = user,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<YamhilliaUser> GetUserFromContext(ClaimsPrincipal contextUser)
        {
            if(contextUser != null)
            {
                var id = contextUser.FindFirstValue(ClaimTypes.NameIdentifier);
                if(!string.IsNullOrEmpty(id))
                {
                    return await userService.GetUserById(id);
                }
            }

            return null;
        }

    }
}