using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MusicStore.BusinessLogic;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Models;

namespace MusicStore2.Controllers
{
    public class HomeController : Controller
    {
        // private readonly IProductRepository _productRepository;

        // public HomeController(IProductRepository productRepository)
        // {
        //     _productRepository = productRepository;
        // }

        private readonly IProduct _product;

        public HomeController()
        {
            var bl = new BusinessLogic();
            _product = new ProductBl();
        }

        public ActionResult Index()
        {
            var products = _product.GetAll() ?? new List<ProductData>();
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
        public ActionResult PrivacyPolicy()
        {
            return View("~/Views/StaticPages/PrivacyPolicy.cshtml");
        }
        public ActionResult TermsOfService()
        {
            return View("~/Views/StaticPages/TermsOfService.cshtml");
        }

        [HttpGet]
        public ActionResult LoadAllNewReleases()
        {
            var allProducts = _product.GetAll();
            var orderedProducts = allProducts?
                .OrderByDescending(p => p.UploadDate)
                .ToList() ?? new List<ProductData>();

            return PartialView("~/Views/Home/_NewReleasesPartial.cshtml", orderedProducts);
        }
    }
}