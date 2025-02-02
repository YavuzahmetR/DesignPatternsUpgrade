using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repos.Decorator
{
    public class ProductRepositoryCacheDecorator : BaseProductRepositoryDecorator
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "Products";
        public ProductRepositoryCacheDecorator(IProductRepository repository, IMemoryCache cache) : base(repository)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(repository));
        }

        public async override Task<List<Product>> GetProductsAsync(string userId)
        {
            var products = await GetProductsAsync();
            return products.Where(p => p.UserId == userId).ToList();    
        }
        public override async Task<List<Product>> GetProductsAsync()
        {
            if (_cache.TryGetValue(CacheKey, out List<Product> cachedProducts))
            {
                return cachedProducts!;
            }
            await UpdateCache();
            return _cache.Get<List<Product>>(CacheKey);
        }
        public override async Task<Product> AddProductAsync(Product product)
        {
            await base.AddProductAsync(product);
            await UpdateCache();
            return product;
        }
        public override async Task DeleteProductAsync(Product product)
        {
            await base.DeleteProductAsync(product);
            await UpdateCache();    
        }
        public override async Task<Product> GetProductAsync(int id)
        {
            var products = await GetProductsAsync();
            return products.Find(p => p.Id == id)!;
        }
        public override async Task UpdateProductAsync(Product product)
        {
            await base.UpdateProductAsync(product);
            await UpdateCache();
        }

        private async Task UpdateCache()
        {
            _cache.Set(CacheKey, await base.GetProductsAsync());
        }

    }
}
