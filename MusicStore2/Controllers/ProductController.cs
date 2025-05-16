using System.Web.Mvc;

namespace MusicStore2.Controllers
{
    public class ProductController : Controller
    {
        // GET
        public ActionResult ProductPage()
        {
            return View();
        }
    }
}