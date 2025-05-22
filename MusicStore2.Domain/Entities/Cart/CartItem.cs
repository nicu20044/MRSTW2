using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore2.Domain.Entities.Cart
{
	public class CartItem
	{
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public decimal Price { get; set; }
    }
}