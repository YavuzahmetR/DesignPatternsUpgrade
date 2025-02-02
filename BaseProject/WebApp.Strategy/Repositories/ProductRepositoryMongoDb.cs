using MongoDB.Driver;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public class ProductRepositoryMongoDb : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        public ProductRepositoryMongoDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDbConnection");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("ProductDb");
            _products = database.GetCollection<Product>("Products");
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }

        public async Task DeleteProduct(string productId)
        {
            await _products.DeleteOneAsync(x =>x.Id == productId);
        }

        public async Task<List<Product>> GetAllProductsByUserId(string userId)
        {
            return await _products.Find(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _products.Find(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        }
    }
}
