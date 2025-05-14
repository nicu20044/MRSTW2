using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext() : base("name=MusicStoreDB") { }
		public DbSet<AppUser> Users { get; set; }
		public DbSet<ProductData> Products { get; set; }
		
	}
}