using System.Collections.Generic;
using System.Threading.Tasks;
using WebMerchantApi.Entities;
using WebMerchantApi.Models;
using WebMerchantApi.Repositories.Interfaces;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ServiceResponse<List<Order>>> GetAll()
        {
            var response = new ServiceResponse<List<Order>>
            {
                Data = await _orderRepository.GetAll()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Order>>> GetForCurrentUser(string userId)
        {
            var response = new ServiceResponse<List<Order>>
            {
                Data = await _orderRepository.GetAllForUser(userId)
            };

            return response;
        }
    }
}
