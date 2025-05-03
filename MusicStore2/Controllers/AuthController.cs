using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStore.BusinessLogic.Services;
using MusicStore2.Domain.Entities.User;
using MusicStore2.Models;


namespace MusicStore2.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
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

    
            Session["UserId"] = response.Id;
            Session["UserEmail"] = response.Email;
            Session["Username"] = response.UserName;
            Session["UserType"] = response.UserRole;
            Session["UserLoginTime"] = response.LoginDateTime;
            Session["Token"] = response.Token;

            string redirectUrl;

            switch (response.UserRole)
            {
                case "Admin":
                    redirectUrl = Url.Action("Dashboard", "Admin");
                    break;
                case "Artist":
                    redirectUrl = Url.Action("Index", "Home");
                    break;
                case "Listener":
                    redirectUrl = Url.Action("Index", "Home");
                    break;
                default:
                    redirectUrl = Url.Action("Login", "Auth");
                    break;
            }

            return Json(new { success = true, redirectUrl });
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

            if (!response.Status)
            {
                TempData["ErrorMessage"] = response.StatusMsg;
                return Json(new { success = false, message = "Wrong Status Message for Register" });
            }

            Session["UserId"] = response.Id;
            Session["UserEmail"] = response.Email;
            Session["Username"] = response.UserName;
            Session["UserType"] = response.UserRole;
            Session["UserLoginTime"] = response.LoginDateTime;
            Session["Token"] = response.Token;

            TempData["SuccessMessage"] = "Cont creat cu succes! Acum sunteți autentificat.";

            switch (response.UserRole)
            {
                case "Admin":
                    return RedirectToAction("Dashboard", "Admin");
                case "Artist":
                    return RedirectToAction("Index", "Home");
                case "Listener":
                    return RedirectToAction("Index", "Home");
                default:
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