using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ActionResult Index()
        {
            var products = _productRepository.GetAllAsyncFromDatabase() ?? new List<ProductData>();
            return View(products);
        }

        public ActionResult About()
        {
            return View("~/Views/StaticPages/AboutUs.cshtml");
        }

        public ActionResult Contact()
        {
            return View("~/Views/StaticPages/Contact.cshtml");
        }

        [HttpGet]
        public ActionResult LoadAllNewReleases()
        {
            var allProducts = _productRepository.GetAllAsyncFromDatabase();
            var orderedProducts = allProducts?
                .OrderByDescending(p => p.UploadDate)
                .ToList() ?? new List<ProductData>();

            return PartialView("~/Views/Home/_NewReleasesPartial.cshtml", orderedProducts);
        }
    }
}
