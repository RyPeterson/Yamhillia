using System;
using System.Security.Claims;
using System.Threading.Tasks;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services.Auth
{
    public interface IAuthenticationService
    {
         Task<string> CreateToken(LoginModel loginModel);

         Task<UserAndToken> Login(LoginModel loginModel);

         UserAndToken Login(YamhilliaUser user);

         Task<YamhilliaUser> GetUserFromContext(ClaimsPrincipal contextUser);
    }   

}