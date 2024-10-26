using DotNetExample_API.Domain;
using MongoDB.Driver;

namespace DotNetExample_API.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task CreateAsync(Product product);
        Task UpdateAsync(string id, Product product);
        Task DeleteAsync(string id);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(MongoDbContext context)
        {
            _products = context.GetCollection<Product>("Products"); // Nome da coleção
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _products.Find(product => true).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(string id, Product product)
        {
            await _products.ReplaceOneAsync(prod => prod.Id == id, product);
        }

        public async Task DeleteAsync(string id)
        {
            await _products.DeleteOneAsync(product => product.Id == id);
        }
    }
}