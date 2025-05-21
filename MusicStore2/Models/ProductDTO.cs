using System.Web;

namespace MusicStore2.Models
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int Bpm { get; set; }
        public string Scale { get; set; }
        public string License { get; set; }
        public string ArtistName { get; set; }
        public int ArtistId { get; set; }
        public int ProducerId { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase AudioFile { get; set; }
    }
}