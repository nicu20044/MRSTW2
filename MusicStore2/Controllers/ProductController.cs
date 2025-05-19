using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore.BusinessLogic.Services;
using MusicStore.BusinessLogic.Services.Interfaces;


namespace MusicStore2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return HttpNotFound("Product not found");
            }

            return View("ProductPage", product);
        }
    }
}