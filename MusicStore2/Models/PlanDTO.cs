using System.ComponentModel.DataAnnotations;

namespace MusicStore2.Models
{
    public class PlanDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Plan name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 9999.99, ErrorMessage = "Enter a valid price.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 3650, ErrorMessage = "Duration must be between 1 and 3650 days.")]
        public int DurationDays { get; set; }
    }
}