using System.Collections.Generic;
using System.Threading.Tasks;
using WebMerchantApi.Entities;
using WebMerchantApi.Models;

namespace WebMerchantApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetAll();

        Task<ServiceResponse<string>> Add(Product product);

        Task<ServiceResponse<bool>> Remove(string productId);
    }
}
