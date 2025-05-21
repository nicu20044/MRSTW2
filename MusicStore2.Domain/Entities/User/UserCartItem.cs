using MusicStore2.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore2.Domain.Entities.User
{
    public class UserCartItem
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }

       
        public virtual AppUser User { get; set; }
        public virtual ProductData Product { get; set; }
    }
}