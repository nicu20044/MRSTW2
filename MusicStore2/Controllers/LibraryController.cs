using System;
using System.IO;
using System.Linq;
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
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var role = Session["UserRole"]?.ToString();
            
            if (role != "Artist" && role != "Admin")
            {
                filterContext.Result = new HttpNotFoundResult();
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        public ActionResult Library()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            int artistId = Convert.ToInt32(Session["UserId"]);

            var productsDomain = _product.GetAll()
                    .Where(p => p.ArtistId == artistId)
                    .ToList();
                
            var productsDto = productsDomain.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                ArtistName = p.ArtistName,
                ImageUrl = p.ImageUrl,
                AudioFileUrl = p.AudioFileUrl,
                Price = p.Price,
                UploadDate = p.UploadDate
            }).ToList();

            return View(productsDto);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductDTO dto)
        {
            if (ModelState.IsValid)
            {
                string audioUrl = null;
                string imageUrl = null;
                
                if (dto.AudioFile != null && dto.AudioFile.ContentLength > 0)
                {
                    string audioFileName = Path.GetFileName(dto.AudioFile.FileName);
                    string audioUploadPath = Server.MapPath("~/UploadedAudios/");
                    if (!Directory.Exists(audioUploadPath))
                    {
                        Directory.CreateDirectory(audioUploadPath);
                    }

                    string audioFilePath = Path.Combine(audioUploadPath, audioFileName);
                    dto.AudioFile.SaveAs(audioFilePath);

                    audioUrl = "~/UploadedAudios/" + audioFileName;
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

                int artistId = Convert.ToInt32(Session["UserId"]);
                string artistName = Session["Username"]?.ToString();

                
                var product = MapDtoToProductData(dto, audioUrl, imageUrl, artistId, artistName);

                _product.Create(product);

                return RedirectToAction("Library", "Library");
            }

            return View(dto);
        }

        private ProductData MapDtoToProductData(ProductDTO dto, string audioUrl, string imageUrl, int artistId, string artistName)
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
                ArtistName = artistName,
                ArtistId = artistId,
                ProducerId = artistId,
                AudioFileUrl = audioUrl,
                ImageUrl = imageUrl,
                UploadDate = DateTime.Now
            };
        }
    }
}