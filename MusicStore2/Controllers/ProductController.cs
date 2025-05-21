using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStore.BusinessLogic;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore.BusinessLogic.Services;
using MusicStore.BusinessLogic.Services.Interfaces;


namespace MusicStore2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _productService;

        public ProductController()
        {
            var bl = new BusinessLogic();
            _productService = new ProductBl();
        }

        public async Task<ActionResult> Details(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return HttpNotFound("Product not found");
            }

            return View("ProductPage", product);
        }
    }
}