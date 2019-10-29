using System;
using System.Threading.Tasks;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services.Auth
{
    public interface IAuthenticationService
    {
         Task<string> CreateToken(LoginModel loginModel);

         Task<UserAndToken> Login(LoginModel loginModel);
    }   

}