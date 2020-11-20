using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMerchantApi.Entities;
using WebMerchantApi.Models;
using WebMerchantApi.Models.Dto;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(ServiceResponse<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAll();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(ProductDto request)
        {
            var response = await _productService.Add(new Product{ Name = request.Name, Price = request.Price});

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove(string productId)
        {
            var response = await _productService.Remove(productId);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
