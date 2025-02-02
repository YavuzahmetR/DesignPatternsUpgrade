using IdentityBase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repos
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly IdentityContext _context;
        private readonly DbSet<Product> _products;
        public ProductRepository(IdentityContext _context)
        {
            this._context = _context;
            _products = _context.Set<Product>();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsAsync(string userId)
        {
            return await _products.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await _products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }
            return product;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var entry = await _products.AddAsync(product);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
