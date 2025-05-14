using System;

namespace MusicStore2.Domain.Entities.User
{
    public class UserAuthResp
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
        
        public string UserRole { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime LoginDateTime { get; set; }
        public string PasswordHash { get; set; }
    }
}