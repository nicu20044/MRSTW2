using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStore.BussinesLogic.Interfaces;
using MusicStore.web.Models;
using MusicStore.Domain.Entities.Product;

namespace MusicStore.web.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IProduct _product;

        public LibraryController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBl();
        }

        public ActionResult Library()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imagePath = "";
            var audioPath = "";
    
            var imagesFolder = Server.MapPath("~/Content/Uploads/Images");
            var audioFolder = Server.MapPath("~/Content/Uploads/Audio");
            
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            if (!Directory.Exists(audioFolder))
            {
                Directory.CreateDirectory(audioFolder);
            }


            if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.ImageFile.FileName); 
                imagePath = Path.Combine("/Content/Uploads/Images", fileName); 
                var serverPath = Path.Combine(imagesFolder, fileName); 


                model.ImageFile.SaveAs(serverPath);
            }

            // Salvarea fișierului audio
            if (model.AudioFile != null && model.AudioFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.AudioFile.FileName); 
                audioPath = Path.Combine("/Content/Uploads/Audio", fileName); 
                var serverPath = Path.Combine(audioFolder, fileName); 

                
                model.AudioFile.SaveAs(serverPath);
            }


            var product = new ProductData
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Genre = model.Genre,
                Bpm = model.Bpm,
                Scale = model.Scale,
                License = model.License,
                ArtistName = model.ArtistName,
                ArtistId = model.ArtistId,
                ProducerId = model.ProducerId,
                ImageUrl = imagePath, 
                AudioFileUrl = audioPath, 
                UploadDate = DateTime.Now
            };

            await _product.AddProduct(product);
            
            return RedirectToAction("Index", "Home");
        }
    }


}
