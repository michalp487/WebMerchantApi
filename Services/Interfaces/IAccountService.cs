using System.Threading.Tasks;
using WebMerchantApi.Models;

namespace WebMerchantApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse<int>> Register(ApplicationUser user, string password);

        Task<ServiceResponse<string>> Login(string username, string password);

        Task<bool> UserExists(string username);
    }
}
