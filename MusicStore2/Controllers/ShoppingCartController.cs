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

        public ShoppingCartController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _cart = bl.GetCartBl();
            _product = bl.GetProductBl(); 
        }

        public async Task<ActionResult> ShoppingCart()
        {
            var userId = Session["UserId"];
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var cartItems = await _cart.GetCartItemsAsync((int)userId);
            return View(cartItems);
        }

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

        public async Task<ActionResult> PaymentPage()
        {
            var userId = Session["UserId"];
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var cartItems = await _cart.GetCartItemsAsync((int)userId);
            var total = cartItems.Sum(item => item.Price); 

            ViewBag.TotalPrice = total;
            return View(cartItems);
        }




       

    }
}
