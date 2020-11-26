using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMerchantApi.Data;
using WebMerchantApi.Entities;
using WebMerchantApi.Repositories.Interfaces;

namespace WebMerchantApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<List<Order>> GetAllForUser(string userId)
        {
            return await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<string> Add(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }

        public async Task<string> Update(Order order)
        {
            _context.Orders.Update(order);

            await _context.SaveChangesAsync();

            return order.Id;
        }
    }
}
