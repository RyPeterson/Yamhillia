using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YamhilliaNET.Services.User;

namespace YamhilliaNET.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private static readonly int EXPIRATION_MINUTES = 30;

        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        public AuthenticationService(IConfiguration config, IUserService userService)
        {
            this.configuration = config;
            this.userService = userService;
        }

        public async Task<string> CreateToken(LoginModel loginModel)
        {
            var user = await userService.GetUserByUsernameAndPassword(loginModel.Username, loginModel.Password);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // PedRelax: server is running therefore the key exists
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["JWT:Issuer"], 
                configuration["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(EXPIRATION_MINUTES),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}