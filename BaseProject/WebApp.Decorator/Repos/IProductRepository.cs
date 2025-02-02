using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repos
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<List<Product>> GetProductsAsync(string userId);
        Task<Product> GetProductAsync(int id);

        Task<Product> AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(Product product);
    }
}
