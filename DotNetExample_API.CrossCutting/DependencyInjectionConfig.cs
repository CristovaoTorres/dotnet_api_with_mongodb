using DotNetExample.Business;
using DotNetExample_API.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetExample_API.CrossCutting
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Registrar as dependências globais
            services.AddScoped<IProdutoBusiness, ProdutoBusiness>();
            services.AddSingleton<ProductRepository, ProductRepository>();
            // Outras configurações de dependências
        }
    }
}
