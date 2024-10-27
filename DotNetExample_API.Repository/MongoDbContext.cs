using MongoDB.Driver;

namespace DotNetExample_API.Repository
{
    // Classe responsável por gerenciar o contexto do MongoDB para o projeto.
    // O MongoDbContext permite a criação de uma conexão com o banco de dados e fornece acesso às coleções.
    public class MongoDbContext
    {
        // Campo privado que armazena a referência ao banco de dados MongoDB.
        private readonly IMongoDatabase _database;

        // Construtor da classe MongoDbContext que inicializa a conexão com o banco.
        // Recebe como parâmetros a string de conexão e o nome do banco de dados.
        public MongoDbContext(string connectionString, string databaseName)
        {
            // Cria um cliente do MongoDB usando a string de conexão.
            var client = new MongoClient(connectionString);

            // A partir do cliente, acessa o banco de dados especificado.
            _database = client.GetDatabase(databaseName);

            // Chama o método para verificar se as coleções necessárias existem;
            // caso contrário, cria as coleções.
            CreateCollectionsIfNotExists();
        }

        // Método privado que verifica a existência das coleções e as cria, se necessário.
        // Esse método é útil para garantir que o banco esteja sempre preparado para o uso.
        private void CreateCollectionsIfNotExists()
        {
            // Obtém a lista de coleções existentes no banco de dados.
            var collections = _database.ListCollectionNames().ToList();

            // Verifica se a coleção "Products" existe. Se não existir, cria a coleção.
            if (!collections.Contains("Products"))
            {
                _database.CreateCollection("Products");
            }

            // Adicione outras coleções conforme necessário, usando a mesma lógica:
            // Exemplo:
            // if (!collections.Contains("OutraColecao"))
            // {
            //     _database.CreateCollection("OutraColecao");
            // }
        }

        // Método público que retorna uma coleção do MongoDB com o tipo especificado.
        // Isso facilita o acesso aos dados de uma coleção específica em outras partes do código.
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
