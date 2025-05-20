using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.BusinessLogic.Data;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore.BusinessLogic.Data.Repositories;
using MusicStore.BusinessLogic.Services;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Domain.Entities.User;


namespace MusicStore2.Controllers
{
    public class AdminController : Controller
    {
        private readonly IGenericRepository<ProductData> _productRepository;
        private readonly IGenericRepository<AppUser> _userRepository;

        public AdminController(IGenericRepository<ProductData> productRepository,
            IGenericRepository<AppUser> userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
 

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult ManageContent()
        {
            var products = _productRepository.GetAllAsyncFromDatabase().ToList();
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
                _productRepository.Add(model);
                _productRepository.Save();

                return RedirectToAction("ManageContent");
            }

            return View(model);
        }


        public ActionResult EditProduct(int id)
        {
            var product = _productRepository.GetById(id);
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
                _productRepository.Update(product);
                _productRepository.Save();
                return RedirectToAction("ManageContent");
            }
            return View(product);
        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found." });
            }

            _productRepository.Delete(product.Id);
            _productRepository.Save();

            return Json(new { success = true });
        }


        public ActionResult ManageUsers()
        {
            var users = _userRepository.GetAllAsyncFromDatabase().ToList();
            return View(users);
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(AppUser user)
        {
            if (ModelState.IsValid)
            {
                string hashed_password = AuthService.ComputeHash(user.PasswordHash);
                user.LastLoginTime = DateTime.Now;
                user.Token = Guid.NewGuid().ToString();
                user.PasswordHash = hashed_password;
                _userRepository.Add(user);
                _userRepository.Save();
                return RedirectToAction("ManageUsers");
            }
            return View(user);
        }

        public ActionResult EditUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(AppUser user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Update(user);
                _userRepository.Save();
                return RedirectToAction("ManageUsers");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();
            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        public ActionResult UpdateUserRole(int id, string role)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                user.UserRole = role;
                _userRepository.Update(user);
                _userRepository.Save();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult UpdateProductPrice(int id, decimal price)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                product.Price = price;
                _productRepository.Update(product);
                _productRepository.Save();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        
        [HttpPost]
        public ActionResult MarkNewReleases()
        {
            var allSongs = _productRepository.GetAllAsyncFromDatabase().ToList();
    
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
    
            _productRepository.Save();
            return RedirectToAction("ManageContent");
        }
    }
}