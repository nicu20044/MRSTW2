using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStore.BusinessLogic;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.Cart;

namespace MusicStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICart _cart;
        private readonly IProduct _product;
        private readonly IPlan _plan;

        public ShoppingCartController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _cart = bl.GetCartBl();
            _product = bl.GetProductBl();
            _plan = bl.GetPlanBl();
        }

        public async Task<ActionResult> ShoppingCart()
        {
            var userId = Session["UserId"];
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var cartItems = await _cart.GetCartItemsAsync((int)userId);
            return View(cartItems);
        }
        

        // public ActionResult PaymentPage()
        // {
        //     return View();
        // }

        [HttpPost]
        public async Task<ActionResult> AddToCart(int productId)
        {
            var userId = Session["UserId"];
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var product = await _product.GetById(productId);
            if (product == null)
                return HttpNotFound();

            await _cart.AddToCartAsync((int)userId, productId);

            return RedirectToAction("ShoppingCart");
        }


        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(int productId)
        {
            var userId = Session["UserId"];
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            await _cart.RemoveFromCartAsync((int)userId, productId);
            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public ActionResult ProceedToPayment()
        {
            return RedirectToAction("PaymentPage", "ShoppingCart");
        }
        public async Task<ActionResult> PaymentPage(int? planId)
        {
            var userId = Session["UserId"];
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            if (planId.HasValue)
            {
                var plan = _plan.GetPlan(planId.Value);
                if (plan == null)
                    return HttpNotFound();

                ViewBag.IsPlan = true;
                ViewBag.TotalPrice = plan.Price;
                return View("PaymentPagePlan", plan);
            }
            else
            {
                var cartItems = await _cart.GetCartItemsAsync((int)userId);
                var total = cartItems.Sum(item => item.Price);

                ViewBag.TotalPrice = total;
                ViewBag.IsPlan = false;
                return View(cartItems);
            }
        }



        //download song

        [HttpGet]
        public ActionResult DownloadSong(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return HttpNotFound("File name is empty.");
            }

            string filePath = Server.MapPath(Path.Combine("~/UploadedAudios", fileName));

            if (!System.IO.File.Exists(filePath))
            {
                return HttpNotFound("File not found at: " + filePath);
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }
        [HttpPost]
        public ActionResult ConfirmPlanPayment(int planId)
        {
            TempData["Message"] = "Payment completed successfully and subscription activated!";
            return RedirectToAction("Index", "Home");
        }

    }
}
