using System.Security.Claims;
using System.Threading.Tasks;

namespace YamhilliaNET.Services.User
{
    public interface IAuthenticationService
    {
        Task<string> GenerateToken(string username, string password);

        Task<ClaimsPrincipal> GenerateClaim(string username, string password);
    }
}