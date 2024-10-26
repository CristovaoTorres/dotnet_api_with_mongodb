using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
