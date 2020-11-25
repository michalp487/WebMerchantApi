using System.Linq;
using System.Threading.Tasks;
using WebMerchantApi.Entities;
using WebMerchantApi.Models;
using WebMerchantApi.Repositories.Interfaces;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IProductRepository _productRepository;

        public BasketService(IBasketItemRepository basketItemRepository, IProductRepository productRepository)
        {
            _basketItemRepository = basketItemRepository;
            _productRepository = productRepository;
        }

        public async Task<ServiceResponse<BasketSummary>> GetSummary()
        {
            var response = new ServiceResponse<BasketSummary>();

            var basketItems = await _basketItemRepository.GetAll();

            var basketSummary = new BasketSummary
            {
                BasketItems = basketItems,
                Amount = basketItems.Sum(x => x.Quantity * x.Price)
            };

            response.Data = basketSummary;

            return response;
        }

        public async Task<ServiceResponse<bool>> Add(string productId)
        {
            var response = new ServiceResponse<bool>();

            var product = await _productRepository.GetById(productId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Could not find product to add.";
                return response;
            }

            var basketItem = await _basketItemRepository.GetByName(product.Name);

            if (basketItem == null)
            {
                basketItem = new BasketItem
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                };

                await _basketItemRepository.Add(basketItem);
            }
            else
            {
                basketItem.Quantity++;

                await _basketItemRepository.Update(basketItem);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> Remove(string productId)
        {
            var response = new ServiceResponse<bool>();

            var product = await _productRepository.GetById(productId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Nothing to remove.";
                return response;
            }

            var basketItem = await _basketItemRepository.GetByName(product.Name);

            await _basketItemRepository.Remove(basketItem);

            return response;
        }

        public async Task<ServiceResponse<bool>> Checkout()
        {
            var basketSummary = await GetSummary();

            var amount = basketSummary.Data.Amount;


            //await _basketItemRepository.RemoveAll();

            return new ServiceResponse<bool>();
        }
    }
}
