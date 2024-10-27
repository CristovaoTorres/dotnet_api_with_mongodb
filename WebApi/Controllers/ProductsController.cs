using DotNetExample.Business;
using DotNetExample_API.Domain;
using DotNetExample_API.Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        public ProductsController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _productBusiness.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            return Ok(await _productBusiness.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(ProductCreateRequest model)
        {
            return Ok(await _productBusiness.CreateAsync(model));
        }

        //Por questões de segurança não passo o Id na rota e sim dentro do body
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateRequest model)
        {
            return Ok(await _productBusiness.UpdateAsync(model));
        }

        //Por questões de segurança não passo o Id na rota e sim dentro do body
        [HttpDelete]
        public async Task<IActionResult> Delete(ProductDeleteRequest model)
        {
            return Ok(await _productBusiness.DeleteAsync(model));
        }
    }
}
