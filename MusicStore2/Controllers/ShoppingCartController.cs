using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore2.Controllers{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult ShoppingCart()
        {
            return View();
        }

        public ActionResult PaymentPage()
        {
            return View();
        }

        public ActionResult Subscribtion()
        {
            return View();
        }
    }
}