using System.Collections.Generic;
using System.Threading.Tasks;
using WebMerchantApi.Entities;
using WebMerchantApi.Models;
using WebMerchantApi.Repositories.Interfaces;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ServiceResponse<List<Product>>> GetAll()
        {
            var response = new ServiceResponse<List<Product>>();

            var products = await _productRepository.GetAll();

            response.Data = products;

            return response;
        }

        public async Task<ServiceResponse<string>> Add(Product product)
        {
            var response = new ServiceResponse<string>();

            if (product.Price <= 0)
            {
                response.Success = false;
                response.Message = "Price needs to be positive number.";
                return response;
            }

            var existingProduct = await _productRepository.GetByName(product.Name);

            if (existingProduct != null)
            {
                response.Success = false;
                response.Message = "Product already exists.";
                return response;
            }

            var productId = await _productRepository.Add(product);

            response.Data = productId;

            return response;
        }

        public async Task<ServiceResponse<bool>> Remove(string productId)
        {
            var response = new ServiceResponse<bool>();

            var existingProduct = await _productRepository.GetById(productId);

            if (existingProduct == null)
            {
                response.Success = false;
                response.Message = "Nothing to remove.";
                return response;
            }

            await _productRepository.Remove(productId);

            response.Data = true;

            return response;
        }
    }
}
