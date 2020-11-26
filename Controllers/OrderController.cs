using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMerchantApi.Constants;
using WebMerchantApi.Entities;
using WebMerchantApi.Enums;
using WebMerchantApi.Models;
using WebMerchantApi.Models.Dto;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet("all")]
        [ProducesResponseType(typeof(ServiceResponse<List<OrderResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _orderService.GetAll();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            var responseDto = new ServiceResponse<List<OrderResponseDto>>
            {
                Data = _mapper.Map<List<OrderResponseDto>>(response.Data)
            };

            return Ok(responseDto);
        }

        [Authorize(Roles = RoleConstants.Customer)]
        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<List<OrderResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var response = await _orderService.GetForCurrentUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!response.Success)
            {
                return BadRequest(response);
            }

            var responseDto = new ServiceResponse<List<OrderResponseDto>>
            {
                Data = _mapper.Map<List<OrderResponseDto>>(response.Data)
            };

            return Ok(responseDto);
        }
    }
}
