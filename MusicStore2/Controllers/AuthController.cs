using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore.BusinessLogic.Services;
using MusicStore.BusinessLogic.Services.Interfaces;
using MusicStore2.Domain.Entities.User;


namespace MusicStore2.Controllers
{
   public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
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

         
            var token = await _authService.CreateUserSessionAsync(response.Id);
            
            Session["UserId"] = response.Id;
            Session["UserEmail"] = response.Email;
            Session["Username"] = response.UserName;
            Session["UserRole"] = response.UserRole;
            Session["UserLoginTime"] = response.LoginDateTime;
            Session["Token"] = token;
            
            var cookie = new HttpCookie("AuthToken", token)
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(2)
            };
            Response.Cookies.Add(cookie);

         
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

            if (!response.Status)
            {
                TempData["ErrorMessage"] = response.StatusMsg;
                return Json(new { success = false, message = response.StatusMsg });
            }

           
            var token = await _authService.CreateUserSessionAsync(response.Id);

            Session["UserId"] = response.Id;
            Session["UserEmail"] = response.Email;
            Session["Username"] = response.UserName;
            Session["UserRole"] = response.UserRole;
            Session["UserLoginTime"] = response.LoginDateTime;
            Session["Token"] = token;

            var cookie = new HttpCookie("AuthToken", token)
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(2)
            };
            Response.Cookies.Add(cookie);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();

            if (Request.Cookies["AuthToken"] != null)
            {
                var cookie = new HttpCookie("AuthToken")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Login");
        }
    }
}