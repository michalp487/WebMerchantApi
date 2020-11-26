using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMerchantApi.Constants;
using WebMerchantApi.Models;
using WebMerchantApi.Models.Dto;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        [Authorize(Roles = RoleConstants.Customer)]
        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<BasketSummaryResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _basketService.GetSummary();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            var responseDto = new ServiceResponse<BasketSummaryResponseDto>
            {
                Data = _mapper.Map<BasketSummaryResponseDto>(response.Data)
            };

            return Ok(responseDto);
        }

        [Authorize(Roles = RoleConstants.Customer)]
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(BasketItemDto request)
        {
            var response = await _basketService.Add(request.ProductId);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = RoleConstants.Customer)]
        [HttpPost("checkout")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add()
        {
            var response = await _basketService.Checkout(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = RoleConstants.Customer)]
        [HttpDelete]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove(string basketItemId)
        {
            var response = await _basketService.Remove(basketItemId);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
