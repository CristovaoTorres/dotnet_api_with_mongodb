using DotNetExample.Business;
using DotNetExample_API.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetExample_API.CrossCutting
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<MongoDbContext>(provider =>
            {
                var connectionString = "mongodb://localhost:27017"; // Use sua string de conexão real
                var databaseName = "mongo"; // Use o nome do seu banco de dados
                return new MongoDbContext(connectionString, databaseName);
            });

            // Registrar as dependências globais
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            // Outras configurações de dependências
        }
    }
}
