using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MusicStore.BusinessLogic;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore.BusinessLogic.Services;
using MusicStore.BusinessLogic.Services.Interfaces;
using MusicStore2.Domain.Entities.User;
using MusicStore2.Models;


namespace MusicStore2.Controllers
{
   public class AuthController : Controller
   {
       private readonly IAuth _authService;
       private readonly ISession _session;

        public AuthController()
        {
            var bl = new BusinessLogic();
            _authService = bl.GetAuthBl();
            _session = bl.GetSessionBl();
        }
        
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAction(UserLoginData model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Date invalide." });

            string dataEmail = model.Email;
            UserAuthResp response = await _authService.LoginAction(model,dataEmail);

            if (!response.Status)
                return Json(new { success = false, message = response.StatusMsg });

         
            var token = await _session.CreateUserSession(response.Id);
            
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

            string dataEmail = model.Email;
            var response = await _authService.UserRegisterAction(model,dataEmail);

            if (!response.Status)
            {
                TempData["ErrorMessage"] = response.StatusMsg;
                return Json(new { success = false, message = response.StatusMsg });
            }

           
            var token = await _session.CreateUserSession(response.Id);

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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePasswordAction(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Date invalide pentru schimbarea parolei." });

            var updateResult = await _authService.ChangeUserPassword(model.Email, model.NewPassword);
            if (!updateResult.Status)
                return Json(new { success = false, message = updateResult.StatusMsg });

            return RedirectToAction("Index", "Home");
        }

    }
}