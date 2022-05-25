using System.Threading.Tasks;
using WebApplication3._1.Models;

namespace WebApplication3._1.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);

    }
}
