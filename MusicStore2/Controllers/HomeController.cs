using System.Collections.Generic;
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
    }
}