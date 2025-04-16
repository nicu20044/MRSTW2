using System.Web.Mvc;

namespace MusicStore.web.Controllers
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