using System.Web.Mvc;

namespace MusicStore2.Controllers
{
    public class BeatsController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}