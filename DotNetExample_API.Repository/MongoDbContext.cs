using MongoDB.Driver;

namespace DotNetExample_API.Repository
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);

            // Verifica se as coleções já existem; caso contrário, cria.
            CreateCollectionsIfNotExists();
        }

        private void CreateCollectionsIfNotExists()
        {
            // Nome da coleção de produtos, por exemplo
            var collections = _database.ListCollectionNames().ToList();
            if (!collections.Contains("Products"))
            {
                _database.CreateCollection("Products");
            }

            // Adicione outras coleções conforme necessário
            // Exemplo:
            // if (!collections.Contains("OutraColecao"))
            // {
            //     _database.CreateCollection("OutraColecao");
            // }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
