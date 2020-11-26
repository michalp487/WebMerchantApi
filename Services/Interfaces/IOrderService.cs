using System.Collections.Generic;
using System.Threading.Tasks;
using WebMerchantApi.Entities;
using WebMerchantApi.Models;

namespace WebMerchantApi.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<Order>>> GetAll();

        Task<ServiceResponse<List<Order>>> GetForCurrentUser(string userId);
    }
}
