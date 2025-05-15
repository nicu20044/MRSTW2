using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace MusicStore2.Domain.Entities.User
{
    public class AppUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
        
        
        public string Token { get; set; }
        
        [Required]
        [Display(Name="Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string PasswordHash { get; set; }
        
        [Display(Name="User_Role")]
        public string UserRole { get; set; }
        
        [Display(Name="Last_Login_Time")]
        public DateTime LastLoginTime { get; set; }
    }
}