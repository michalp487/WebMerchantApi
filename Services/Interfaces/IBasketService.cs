using System.Threading.Tasks;
using WebMerchantApi.Models;

namespace WebMerchantApi.Services.Interfaces
{
    public interface IBasketService
    {
        Task<ServiceResponse<BasketSummary>> GetSummary();

        Task<ServiceResponse<bool>> Add(string productId);

        Task<ServiceResponse<bool>> Remove(string productId);

        Task<ServiceResponse<bool>> Checkout(string userId);
    }
}
