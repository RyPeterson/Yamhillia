using System;
using System.Threading.Tasks;
namespace YamhilliaNET.Services.Auth
{
    public interface IAuthenticationService
    {
         Task<string> CreateToken(LoginModel loginModel);
    }

}