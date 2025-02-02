using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repos.Decorator
{
    public class BaseProductRepositoryDecorator : IProductRepository
    {
        private readonly IProductRepository _repository;
        public BaseProductRepositoryDecorator(IProductRepository repository)
        {
            _repository = repository;
        }
        public virtual async Task<Product> AddProductAsync(Product product)
        {
            return await _repository.AddProductAsync(product);
        }
        public virtual async Task DeleteProductAsync(Product product)
        {
             await _repository.DeleteProductAsync(product);
        }

        public virtual async Task<Product> GetProductAsync(int id)
        {
            return await _repository.GetProductAsync(id);
        }

        public virtual async Task<List<Product>> GetProductsAsync()
        {
            return await _repository.GetProductsAsync();
        }

        public virtual async Task<List<Product>> GetProductsAsync(string userId)
        {
            return await _repository.GetProductsAsync(userId);
        }

        public virtual async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateProductAsync(product);
        }
    }
}
