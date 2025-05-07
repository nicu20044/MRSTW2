using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStore.BusinessLogic.Services;
using MusicStore2.Domain.Entities.User;


namespace MusicStore2.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> LoginAction(UserLoginData model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Date invalide." });

            var response = await _authService.UserLoginActionAsync(model);

            if (!response.Status)
                return Json(new { success = false, message = response.StatusMsg });
            // //
            // //
            // // Session["UserId"] = response.Id;
            // // Session["UserEmail"] = response.Email;
            // // Session["Username"] = response.UserName;
            // // Session["UserType"] = response.UserRole;
            // // Session["UserLoginTime"] = response.LoginDateTime;
            // // Session["Token"] = response.Token;
            //
            // string redirectUrl;

            if (response.UserRole == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (response.UserRole == "Artist")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAction(UserRegData model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Date invalide. Verificați câmpurile introduse.";
                return Json(new { success = false, message = "Date invalide." });
            }

            var response = await _authService.UserRegisterActionAsync(model);

            TempData["SuccessMessage"] = "Cont creat cu succes! Acum sunteți autentificat.";

            if (response.UserRole == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (response.UserRole == "Artist")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
         
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}