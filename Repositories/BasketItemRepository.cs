using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebMerchantApi.Data;
using WebMerchantApi.Entities;
using WebMerchantApi.Repositories.Interfaces;

namespace WebMerchantApi.Repositories
{
    public class BasketItemRepository : IBasketItemRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ApplicationDbContext _context;

        public BasketItemRepository(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<List<BasketItem>> GetAll()
        {
            return await _context.BasketItems.Where(x => x.UserId == GetUserId()).ToListAsync();
        }

        public async Task<BasketItem> GetByName(string name)
        {
            return await _context.BasketItems.FirstOrDefaultAsync(x => x.Name == name && x.UserId == GetUserId());
        }

        public async Task<string> Add(BasketItem basketItem)
        {
            basketItem.UserId = GetUserId();

            await _context.BasketItems.AddAsync(basketItem);

            await _context.SaveChangesAsync();

            return basketItem.Id;
        }

        public async Task<string> Update(BasketItem basketItem)
        {
            _context.BasketItems.Update(basketItem);

            await _context.SaveChangesAsync();

            return basketItem.Id;
        }

        public async Task Remove(BasketItem basketItem)
        {
            _context.BasketItems.Remove(basketItem);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAll()
        {
            var basketItemsToRemove = _context.BasketItems.Where(x => x.UserId == GetUserId());

            _context.BasketItems.RemoveRange(basketItemsToRemove);

            await _context.SaveChangesAsync();
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
