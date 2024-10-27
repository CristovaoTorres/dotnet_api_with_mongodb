using DotNetExample_API.Domain;
using DotNetExample_API.Domain.Request;
using DotNetExample_API.Repository;

namespace DotNetExample.Business
{
    // Interface que define as operações de negócio para produtos.
    // Todos os métodos retornam um objeto BaseResponse, que padroniza as respostas.
    public interface IProductBusiness
    {
        Task<BaseResponse> GetAllAsync();
        Task<BaseResponse> GetByIdAsync(string id);
        Task<BaseResponse> CreateAsync(ProductCreateRequest model);
        Task<BaseResponse> UpdateAsync(ProductUpdateRequest model);
        Task<BaseResponse> DeleteAsync(ProductDeleteRequest model);
    }

    // Classe que implementa a lógica de negócios para produtos.
    // Esta classe usa o repositório IProductRepository para acessar e manipular dados no banco.
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;

        // Construtor que recebe o repositório de produtos via injeção de dependência.
        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Método para obter todos os produtos. Retorna uma lista de produtos dentro do BaseResponse.
        public async Task<BaseResponse> GetAllAsync()
        {
            var response = new BaseResponse(); // Instância da resposta padrão

            // Chama o repositório para buscar todos os produtos.
            var retorno = await _productRepository.GetAllAsync();
            response.AddData(retorno); // Adiciona os dados à resposta.

            return response;
        }

        // Método para obter um produto específico pelo ID. 
        public async Task<BaseResponse> GetByIdAsync(string id)
        {
            var response = new BaseResponse();

            // Busca o produto pelo ID usando o repositório.
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                // Adiciona um erro se o produto não for encontrado.
                response.AddError(new ValidationError { ErrorMessage = "Produto não encontrado", Field = "Id" });
                return response;
            }

            response.AddData(product); // Adiciona o produto à resposta.
            return response;
        }

        // Método para criar um novo produto. Recebe um modelo de criação e realiza validações antes de salvar.
        public async Task<BaseResponse> CreateAsync(ProductCreateRequest model)
        {
            var response = new BaseResponse();

            // Validação de regra de negócio: o preço deve ser maior que zero.
            if (model.Price <= 0)
            {
                response.AddError(new ValidationError { ErrorMessage = "O preço do produto deve ser maior que zero.", Field = "Price" });
                return response;
            }

            // Validação de campo obrigatório: o nome do produto não pode ser vazio.
            if (string.IsNullOrEmpty(model.Name))
            {
                response.AddError(new ValidationError { ErrorMessage = "O nome do produto é obrigatório.", Field = "Name" });
                return response;
            }

            // Mapeia o modelo de criação para um objeto Product.
            var product = model.MapToCreate();

            // Chama o repositório para criar o produto.
            await _productRepository.CreateAsync(product);

            response.AddData(product); // Adiciona o produto criado à resposta.
            return response;
        }

        // Método para atualizar um produto existente. Recebe um modelo de atualização.
        public async Task<BaseResponse> UpdateAsync(ProductUpdateRequest model)
        {
            var response = new BaseResponse();

            // Valida se o produto existe antes de atualizar.
            var existingProduct = await _productRepository.GetByIdAsync(model.Id);
            if (existingProduct == null)
            {
                response.AddError(new ValidationError { ErrorMessage = "Produto não encontrado para atualização.", Field = "id" });
                return response;
            }

            // Validação de regra de negócio: o preço deve ser maior que zero.
            if (model.Price <= 0)
            {
                response.AddError(new ValidationError { ErrorMessage = "O preço do produto deve ser maior que zero.", Field = "id" });
                return response;
            }

            // Mapeia o modelo de atualização para um objeto Product.
            var product = model.MapToUpdate();

            // Chama o repositório para atualizar o produto.
            await _productRepository.UpdateAsync(model.Id, product);
            response.AddData(product); // Adiciona o produto atualizado à resposta.
            return response;
        }

        // Método para excluir um produto. Recebe um modelo de exclusão.
        public async Task<BaseResponse> DeleteAsync(ProductDeleteRequest model)
        {
            var response = new BaseResponse();

            // Valida se o produto existe antes de excluir.
            var existingProduct = await _productRepository.GetByIdAsync(model.Id);
            if (existingProduct == null)
            {
                response.AddError(new ValidationError { ErrorMessage = "Produto não encontrado para exclusão", Field = "Id" });
                return response;
            }

            // Chama o repositório para excluir o produto.
            await _productRepository.DeleteAsync(model.Id);

            return response; // Retorna uma resposta padrão indicando sucesso.
        }
    }
}
