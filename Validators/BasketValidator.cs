using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMerchantApi.Models;
using WebMerchantApi.Repositories.Interfaces;
using WebMerchantApi.Validators.Interfaces;

namespace WebMerchantApi.Validators
{
    public class BasketValidator : IBasketValidator
    {
        private readonly IBasketItemRepository _basketItemRepository;

        public BasketValidator(IBasketItemRepository basketItemRepository)
        {
            _basketItemRepository = basketItemRepository;
        }

        public async Task<List<Error>> Validate()
        {
            var response = new List<Error>();

            var basketItems = await _basketItemRepository.GetAll();

            if (basketItems.Any(x => x.Price <= 0))
            {
                response.Add(new Error("Prices must be positive. Please verify your basket."));
            }

            if (basketItems.Sum(x => x.Price * x.Quantity) <= 0)
            {
                response.Add(new Error("Basket amount must be positive. Please verify your basket."));
            }

            if (basketItems.Any(x => x.Quantity <= 0))
            {
                response.Add(new Error("Quantity of items must be positive. Please verify your basket."));
            }

            return response;
        }
    }
}
