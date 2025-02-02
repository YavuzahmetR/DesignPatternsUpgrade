using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public  interface IProductRepository
    {
        Task<List<Product>> GetAllProductsByUserId(string userId);

        Task<Product> GetProductById(string productId);

        Task<Product> CreateProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(string productId);
    }
}
