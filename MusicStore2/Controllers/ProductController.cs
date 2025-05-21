using System.Linq;
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





        [HttpPost]
        public async Task<ActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return RedirectToAction("Index", "Home");

            var results = await _productService.SearchByNameAsync(query);

            if (results.Count() == 1)
            {
                var song = results.First();
                return RedirectToAction("Details", "Product", new { id = song.Id });
            }

            // Multiple results: show a results list (optional)
            return View("SearchResults", results);
        }

    }
}