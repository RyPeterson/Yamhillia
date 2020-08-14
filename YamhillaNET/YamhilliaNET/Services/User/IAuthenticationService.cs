using System.Threading.Tasks;

namespace YamhilliaNET.Services.User
{
    public interface IAuthenticationService
    {
        Task<string> GenerateToken(string username, string password);

    }
}