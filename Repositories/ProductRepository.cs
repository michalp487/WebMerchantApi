using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMerchantApi.Data;
using WebMerchantApi.Entities;
using WebMerchantApi.Repositories.Interfaces;

namespace WebMerchantApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(string productId)
        {
            return await _context.Products.SingleAsync(x => x.Id == productId);
        }

        public async Task<Product> GetByName(string name)
        {
            return await _context.Products.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<string> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task Remove(string productId)
        {
            var product = await _context.Products.SingleAsync(x => x.Id == productId);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}
