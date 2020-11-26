using System.Linq;
using System.Threading.Tasks;
using WebMerchantApi.Entities;
using WebMerchantApi.Models;
using WebMerchantApi.Repositories.Interfaces;
using WebMerchantApi.Services.Interfaces;
using WebMerchantApi.Validators.Interfaces;

namespace WebMerchantApi.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketValidator _basketValidator;

        public BasketService(IBasketItemRepository basketItemRepository, IProductRepository productRepository, IOrderRepository orderRepository, IBasketValidator basketValidator)
        {
            _basketItemRepository = basketItemRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _basketValidator = basketValidator;
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

        public async Task<ServiceResponse<bool>> Remove(string basketItemId)
        {
            var response = new ServiceResponse<bool>();

            var basketItem = await _basketItemRepository.GetById(basketItemId);

            if (basketItem == null)
            {
                response.Success = false;
                response.Message = "Nothing to remove.";
                return response;
            }

            await _basketItemRepository.Remove(basketItem);

            return response;
        }

        public async Task<ServiceResponse<bool>> Checkout(string userId)
        {
            var response = new ServiceResponse<bool>();

            var validationResponse = await _basketValidator.Validate();

            if (validationResponse.Any())
            {
                response.Success = false;
                response.Message = string.Join(", ", validationResponse);
                return response;
            }

            var basketSummary = await GetSummary();

            var order = new Order
            {
                Price = basketSummary.Data.Amount,
                UserId = userId
            };

            await _orderRepository.Add(order);

            await _basketItemRepository.RemoveAll();

            return new ServiceResponse<bool>();
        }
    }
}
