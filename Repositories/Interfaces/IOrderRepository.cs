using System.Collections.Generic;
using System.Threading.Tasks;
using WebMerchantApi.Entities;

namespace WebMerchantApi.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();

        Task<List<Order>> GetAllForUser(string userId);

        Task<string> Add(Order order);

        Task<string> Update(Order order);
    }
}
