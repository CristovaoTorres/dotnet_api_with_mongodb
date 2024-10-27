using DotNetExample.Business;
using DotNetExample_API.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetExample_API.CrossCutting
{
    // Classe estática responsável por configurar a injeção de dependência no projeto.
    // Essa classe centraliza o registro de serviços para que o ASP.NET Core saiba quais dependências fornecer.
    public static class DependencyInjectionConfig
    {
        // Método de extensão para IServiceCollection que registra as dependências usadas no projeto.
        // Este método é chamado no Program.cs para configurar o contêiner de injeção de dependência.
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Registro do MongoDbContext como Singleton, para que apenas uma instância seja criada e compartilhada.
            // Isso é útil para manter a conexão com o MongoDB ao longo de todo o ciclo de vida do aplicativo.
            services.AddSingleton<MongoDbContext>(provider =>
            {
                // Define a string de conexão com o MongoDB.
                // Em um ambiente real, é recomendável usar variáveis de ambiente ou o appsettings.json para segurança.
                var connectionString = "mongodb://localhost:27017"; // Substitua pela sua string de conexão
                var databaseName = "mongo"; // Substitua pelo nome do banco de dados

                // Retorna uma nova instância de MongoDbContext, configurada com a string de conexão e o nome do banco.
                return new MongoDbContext(connectionString, databaseName);
            });

            // Registra a interface IProductBusiness com sua implementação ProductBusiness como Scoped.
            // Scoped significa que uma nova instância será criada para cada solicitação HTTP.
            services.AddScoped<IProductBusiness, ProductBusiness>();

            // Registra a interface IProductRepository com sua implementação ProductRepository como Singleton.
            // Singleton significa que uma única instância será usada durante toda a execução do aplicativo.
            services.AddSingleton<IProductRepository, ProductRepository>();

            // Outras dependências globais podem ser registradas aqui conforme o projeto cresce.
        }
    }
}
