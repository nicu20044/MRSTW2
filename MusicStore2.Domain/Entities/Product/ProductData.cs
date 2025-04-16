using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain.Entities.Product
{
    public class ProductData
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string Genre { get; set; }
        public int ProducerId { get; set; }
        public int Bpm { get; set; }
        [StringLength(255)]
        public string ImageUrl { get; set; }
        public string ReleaseDate { get; set; }
        public string ArtistName { get; set; }
        public string Scale { get; set; }
        public string AudioFileUrl { get; set; }
        public DateTime UploadDate { get; set; }
        public string License { get; set; }
        public int ArtistId { get; set; }
    }
}