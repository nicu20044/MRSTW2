using System.ComponentModel.DataAnnotations;

namespace MusicStore2.Models
{
    public class UserAuth
    {
        public class LoginModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
        public class RegisterModel
        {
        
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [Display(Name = "Password")]
            public string Password { get; set; }
        
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }
        
            [Required]
            [Display(Name = "UserRole")]
            public string UserRole { get; set; }
        
        }
        public class UserAuthResp
        {
            public bool Status { get; set; }
            public string StatusMsg { get; set; }
            public string UserRole { get; set; }
            public string Token { get; set; }
            public string Id { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public System.DateTime LoginDateTime { get; set; }
        }
    }
}