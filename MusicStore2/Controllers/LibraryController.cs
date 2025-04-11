using System.Web.Mvc;

namespace MusicStore2.Controllers
{
    public class LibraryController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}