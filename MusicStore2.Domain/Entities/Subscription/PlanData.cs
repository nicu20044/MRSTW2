using System.ComponentModel.DataAnnotations;

namespace MusicStore2.Domain.Entities.Subscription
{
    public class PlanData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DurationDays { get; set; }
    }
}