using Microsoft.EntityFrameworkCore;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public class ProductRepositorySqlServer : IProductRepository
    {
        private readonly IdentityContext _context;
        private readonly DbSet<Product> _products;
        public ProductRepositorySqlServer(IdentityContext context)
        {
            _context = context;
            _products = context.Set<Product>();
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(string productId)
        {
            var product = await _products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public Task<List<Product>> GetAllProductsByUserId(string userId)
        {
            return _products.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
            var product = await _products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public async Task UpdateProduct(Product product)
        {
            _products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
