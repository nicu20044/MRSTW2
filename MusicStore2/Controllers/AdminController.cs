using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MusicStore.BusinessLogic.Core;
using MusicStore.BusinessLogic.Data;
using MusicStore.BusinessLogic;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Domain.Entities.Subscription;
using MusicStore2.Domain.Entities.User;
using MusicStore2.Models;

namespace MusicStore2.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProduct _product;
        private readonly IUser _user;
        private readonly IPlan _plan;

        public AdminController()
        {
            var bl = new BusinessLogic();
            _product = bl.GetProductBl();
            _user = bl.GetUserBl();
            _plan = bl.GetPlanBl();
        }

        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserRole"]?.ToString() != "Admin")
            {
                filterContext.Result = new HttpNotFoundResult();
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult ManageContent()
        {
            var products = _product.GetAll();
            return View(products);
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductData model, HttpPostedFileBase audioFile, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (audioFile != null && audioFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(audioFile.FileName);
                    string uploadPath = Server.MapPath("~/UploadedAudios/");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string filePath = Path.Combine(uploadPath, fileName);
                    audioFile.SaveAs(filePath);

                    model.AudioFileUrl = "~/UploadedAudios/" + fileName;
                }

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string imageFileName = Path.GetFileName(imageFile.FileName);
                    string imageUploadPath = Server.MapPath("~/UploadedImages/");
                    if (!Directory.Exists(imageUploadPath))
                    {
                        Directory.CreateDirectory(imageUploadPath);
                    }

                    string imageFilePath = Path.Combine(imageUploadPath, imageFileName);
                    imageFile.SaveAs(imageFilePath);

                    model.ImageUrl = "~/UploadedImages/" + imageFileName;
                }

                model.UploadDate = DateTime.Now;
                _product.Create(model);

                return RedirectToAction("ManageContent");
            }

            return View(model);
        }

        public async Task<ActionResult> EditProduct(int id)
        {
            var product = await _product.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductData product)
        {
            if (ModelState.IsValid)
            {
                _product.Update(product);
                return RedirectToAction("ManageContent");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            _product.Delete(id);
            return RedirectToAction("ManageContent");
        }

        public ActionResult ManageUsers()
        {
            var users = _user.GetAll();
            return View(users);
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            _user.DeleteUser(id);
            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUserRole(int id, string role)
        {
            var user = await _user.GetById(id);
            if (user != null)
            {
                user.UserRole = role;
                await _user.UpdateUserRole(user.Email, role);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProductPrice(int id, decimal price)
        {
            var product = await _product.GetById(id);
            if (product != null)
            {
                product.Price = price;
                await _product.Update(product);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult MarkNewReleases()
        {
            var allSongs = _product.GetAll().ToList();

            var latestSongs = allSongs
                .OrderByDescending(p => p.UploadDate)
                .Take(5)
                .Select(product => new
                {
                    product.ImageUrl,
                    product.Name,
                    product.ArtistName,
                    product.AudioFileUrl
                })
                .ToList();
            ViewBag.LatestSongs = latestSongs;

            return RedirectToAction("ManageContent");
        }

        
        
        public ActionResult PlansManagement()
        {
            var plans = _plan.GetAll();
            return View(plans);
        }
        public ActionResult CreatePlan()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlan(PlanDTO planDto)
        {
            if (ModelState.IsValid)
            {
                var planData = new PlanData
                {
                    Id = planDto.Id,
                    Name = planDto.Name,
                    Price = planDto.Price,
                    DurationDays = planDto.DurationDays
                };

                _plan.Create(planData);

                return RedirectToAction("PlansManagement");
            }

            
            return View(planDto);
        }
        public ActionResult Edit(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var plan = _plan.GetPlan(id);
            if (plan == null)
                return HttpNotFound();

            return View(plan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanData plan)
        {
            if (ModelState.IsValid)
            {
                _plan.Update(plan);
                return RedirectToAction("Index");
            }
            return View(plan);
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var plan = _plan.GetPlan(id);
            if (plan == null)
                return HttpNotFound();

            return View(plan);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _plan.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
