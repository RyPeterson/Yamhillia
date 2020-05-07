using System.Threading.Tasks;

namespace YamhillaNET.Services.User
{
    public interface IAuthenticationService
    {
        Task<string> GenerateToken(string username, string password);

    }
}