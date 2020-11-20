using System.Collections.Generic;
using System.Threading.Tasks;
using WebMerchantApi.Entities;

namespace WebMerchantApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

        Task<Product> GetById(string productId);

        Task<Product> GetByName(string name);

        Task<string> Add(Product product);

        Task Remove(string productId);
    }
}
