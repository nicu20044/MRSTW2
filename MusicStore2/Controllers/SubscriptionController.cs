using System.Web.Mvc;
using MusicStore.BusinessLogic;
using MusicStore.BusinessLogic.Interfaces;

namespace MusicStore2.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly IPlan _plan;

        public SubscriptionController()
        {
            var bl = new BusinessLogic();
            _plan = bl.GetPlanBl();
        }
        // GET
        public ActionResult Subscription()
        {
            var plans = _plan.GetAll();
            return View(plans);
        }
        
    }
}