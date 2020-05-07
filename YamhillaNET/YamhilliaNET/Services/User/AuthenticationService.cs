using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using YamhillaNET.Exceptions;
using YamhillaNET.Util;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace YamhillaNET.Services.User
{
    public class AuthenticationService : IAuthenticationService
    {
        private static readonly int TokenDays = 3;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IUserService userService, IOptions<AppSettings> options)
        {
            _userService = userService;
            _appSettings = options.Value;
        }
        
        public async Task<string> GenerateToken(string username, string password)
        {
            var user = await _userService.Authenticate(username, password);
            if (user == null)
            {
                throw new YamhilliaBadRequestException("Invalid credentials");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Name, user.Id.ToString()), }),
                Expires = DateTime.UtcNow.AddDays(TokenDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}