using System.Web.Mvc;
using MusicStore.BusinessLogic;
using MusicStore.BusinessLogic.Interfaces;

namespace MusicStore2.Controllers{
    public class LibraryController : Controller
    {
        private readonly IProduct _product;
        public LibraryController()
        {
            // var bl = new BusinessLogic.BusinessLogic();
            // _product = bl.GetProductBl();
        }
        // GET
        public ActionResult Library()
        {
            return View();
        }
    }
}