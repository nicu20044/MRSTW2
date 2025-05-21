using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MusicStore.BusinessLogic.EntityBL;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore.BusinessLogic.Services.Interfaces;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Models;

namespace MusicStore2.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IProduct _product;

        public LibraryController()
        {
            var bl = new BusinessLogic();
            _product = new ProductBl();
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
        public ActionResult AddProduct(ProductDTO dto)
        {
            string audioUrl = null;
            string imageUrl = null;

            if (ModelState.IsValid)
            {
                if (dto.AudioFile != null && dto.AudioFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(dto.AudioFile.FileName);
                    string uploadPath = Server.MapPath("~/UploadedAudios/");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string filePath = Path.Combine(uploadPath, fileName);
                    dto.AudioFile.SaveAs(filePath);

                    audioUrl = "~/UploadedAudios/" + fileName;
                }

                if (dto.ImageFile != null && dto.ImageFile.ContentLength > 0)
                {
                    string imageFileName = Path.GetFileName(dto.ImageFile.FileName);
                    string imageUploadPath = Server.MapPath("~/UploadedImages/");
                    if (!Directory.Exists(imageUploadPath))
                    {
                        Directory.CreateDirectory(imageUploadPath);
                    }

                    string imageFilePath = Path.Combine(imageUploadPath, imageFileName);
                    dto.ImageFile.SaveAs(imageFilePath);

                    imageUrl = "~/UploadedImages/" + imageFileName;
                }

                ProductData model = MapDtoToProductData(dto, audioUrl, imageUrl);
                _product.Create(model);

                return View("Home");
            }

            return View(dto);
        }

        private ProductData MapDtoToProductData(ProductDTO dto, string audioUrl, string imageUrl)
        {
            return new ProductData
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                Genre = dto.Genre,
                Bpm = dto.Bpm,
                Scale = dto.Scale,
                License = dto.License,
                ArtistName = dto.ArtistName,
                ArtistId = dto.ArtistId,
                ProducerId = dto.ProducerId,
                AudioFileUrl = audioUrl,
                ImageUrl = imageUrl,
                UploadDate = DateTime.Now
            };
        }
    }


}
