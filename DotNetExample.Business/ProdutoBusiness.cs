using DotNetExample_API.Domain;
using DotNetExample_API.Repository;

namespace DotNetExample.Business
{
    public interface IProductBusiness
    {
        Task<BaseResponse> GetAllAsync();
        Task<BaseResponse> GetByIdAsync(string id);
        Task<BaseResponse> CreateAsync(Product product);
        Task<BaseResponse> UpdateAsync(string id, Product product);
        Task<BaseResponse> DeleteAsync(string id);
    }

    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResponse> GetAllAsync()
        {
            var response = new BaseResponse();
            // Você pode adicionar lógica adicional se necessário

            var retorno = await _productRepository.GetAllAsync();
            response.AddData(retorno);

            return response;
        }

        public async Task<BaseResponse> GetByIdAsync(string id)
        {
            var response = new BaseResponse();

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                response.AddError(new ValidationError { ErrorMessage = "Produto não encontrado", Field = "Id" });
                return response;
            }

            response.AddData(product);
            return response;
        }
        public async Task<BaseResponse> CreateAsync(Product product)
        {
            var response = new BaseResponse();


            // Exemplo de regra de negócio: validar preço
            if (product.Price <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.");

            // Exemplo de outra validação
            if (string.IsNullOrEmpty(product.Name))
            {
                response.AddError(new ValidationError { ErrorMessage = "O nome do produto é obrigatório.", Field = "Name" });
                return response;
            }

            await _productRepository.CreateAsync(product);

            response.AddData(product);
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(string id, Product product)
        {
            var response = new BaseResponse();

            // Verifica se o produto existe antes de atualizar
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                response.AddError(new ValidationError { ErrorMessage = "Produto não encontrado para atualização.", Field = "id" });
                return response;
            }


            // Exemplo de validação adicional
            if (product.Price <= 0)
            {
                response.AddError(new ValidationError { ErrorMessage = "O preço do produto deve ser maior que zero.", Field = "id" });
                return response;
            }

            await _productRepository.UpdateAsync(id, product);
            response.AddData(product);
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(string id)
        {
            var response = new BaseResponse();


            // Verifica se o produto existe antes de excluir
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                response.AddError(new ValidationError { ErrorMessage = "Produto não encontrado para exclusão", Field = "Id" });
                return response;
            }

            await _productRepository.DeleteAsync(id);

            return response;
        }
    }
}
