using DotNetExample_API.Domain;

namespace DotNetExample_API.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        // Adicione métodos específicos de Product, se necessário
    }

    // Repositório específico para a entidade Product, herdando métodos CRUD da BaseRepository
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(MongoDbContext context)
            : base(context, "Products") // Define o nome da coleção específica
        {
        }

        // Métodos específicos de Product podem ser adicionados aqui, se necessário
    }
}
