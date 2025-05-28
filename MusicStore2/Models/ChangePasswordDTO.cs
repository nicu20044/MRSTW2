using System.ComponentModel.DataAnnotations;

namespace MusicStore2.Models
{
    public class ChangePasswordDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}


