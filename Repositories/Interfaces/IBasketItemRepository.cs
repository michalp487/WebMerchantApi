using System.Collections.Generic;
using System.Threading.Tasks;
using WebMerchantApi.Entities;

namespace WebMerchantApi.Repositories.Interfaces
{
    public interface IBasketItemRepository
    {
        Task<List<BasketItem>> GetAll();

        Task<BasketItem> GetByName(string name);

        Task<string> Add(BasketItem basketItem);

        Task<string> Update(BasketItem basketItem);

        Task Remove(BasketItem basketItem);

        Task RemoveAll();
    }
}
