using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicStore2.Domain.Entities.User;

namespace MusicStore2.Domain.Entities.Subscription
{
    public class SubscriptionData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PlanName { get; set; }  // ex: Basic, Pro, Premium

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; }
        
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}