using DotNetExample.Business;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetExample_API.CrossCutting
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Registrar as dependências globais
            services.AddScoped<IProdutoBusiness, ProdutoBusiness>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            // Outras configurações de dependências
        }
    }
}
