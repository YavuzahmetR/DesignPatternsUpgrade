using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repos.Decorator
{
    public class ProductRepositoryLoggingDecorator : BaseProductRepositoryDecorator
    {
        private readonly ILogger<ProductRepositoryLoggingDecorator> _logger;

        public ProductRepositoryLoggingDecorator(IProductRepository repository, ILogger<ProductRepositoryLoggingDecorator> logger) : base(repository)
        {
            _logger = logger;
        }

        public override async Task<List<Product>> GetProductsAsync(string userId)
        {
            _logger.LogInformation("Getting products for user {userId}", userId);
            return await base.GetProductsAsync(userId);
        }

        public override async Task<List<Product>> GetProductsAsync()
        {
            _logger.LogInformation("Getting all products");
            return await base.GetProductsAsync();
        }

        public override async Task<Product> AddProductAsync(Product product)
        {
            _logger.LogWarning("Adding product {product}", product.Name);
            return await base.AddProductAsync(product);
        }

        public override async Task DeleteProductAsync(Product product)
        {
            _logger.LogCritical("Deleting product {product}", product.Name);
            await base.DeleteProductAsync(product);
        }

        public override async Task<Product> GetProductAsync(int id)
        {
            _logger.LogInformation("Getting product with id {id}", id);
            return await base.GetProductAsync(id);
        }
        public override async Task UpdateProductAsync(Product product)
        {
            _logger.LogCritical("Updating product {product}", product.Name);
            await base.UpdateProductAsync(product);
        }

    }
}
