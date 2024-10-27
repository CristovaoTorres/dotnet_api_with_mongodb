using DotNetExample.Business;
using DotNetExample_API.Domain;
using DotNetExample_API.Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    // Define a rota base para os endpoints deste controlador. 
    // Todos os endpoints estarão acessíveis a partir de "api/products".
    [Route("api/products")]
    public class ProductsController : Controller
    {
        // Dependência para a camada de negócios que manipula a lógica dos produtos.
        private readonly IProductBusiness _productBusiness;

        // O construtor recebe a instância de IProductBusiness via injeção de dependência.
        // Isso permite que o controlador utilize os métodos de negócio.
        public ProductsController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        // Endpoint para obter todos os produtos cadastrados.
        // [HttpGet] indica que esse método responde a uma requisição GET.
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            // Chama o método de negócio para obter todos os produtos e retorna uma resposta 200 OK com os dados.
            return Ok(await _productBusiness.GetAllAsync());
        }

        // Endpoint para obter um produto específico pelo ID.
        // A {id} na rota indica que o ID será passado como um parâmetro de rota.
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            // Chama o método de negócio para obter um produto pelo ID e retorna uma resposta 200 OK com os dados.
            return Ok(await _productBusiness.GetByIdAsync(id));
        }

        // Endpoint para criar um novo produto.
        // O [HttpPost] indica que esse método responde a uma requisição POST.
        // Recebe um modelo de requisição ProductCreateRequest como corpo da requisição.
        [HttpPost]
        public async Task<ActionResult<Product>> Create(ProductCreateRequest model)
        {
            // Chama o método de negócio para criar o produto e retorna uma resposta 200 OK com o produto criado.
            return Ok(await _productBusiness.CreateAsync(model));
        }

        // Endpoint para atualizar um produto.
        // Este método responde a uma requisição PUT.
        // Observação: Por razões de segurança, o ID do produto não é passado na rota, mas sim dentro do corpo da requisição.
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateRequest model)
        {
            // Chama o método de negócio para atualizar o produto e retorna uma resposta 200 OK.
            return Ok(await _productBusiness.UpdateAsync(model));
        }

        // Endpoint para deletar um produto.
        // Este método responde a uma requisição DELETE.
        // Observação: Assim como no update, o ID é passado no corpo da requisição e não na rota, por motivos de segurança.
        [HttpDelete]
        public async Task<IActionResult> Delete(ProductDeleteRequest model)
        {
            // Chama o método de negócio para deletar o produto e retorna uma resposta 200 OK.
            return Ok(await _productBusiness.DeleteAsync(model));
        }
    }
}
