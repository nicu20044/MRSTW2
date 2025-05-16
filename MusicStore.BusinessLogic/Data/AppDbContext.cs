using System.Data.Entity;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext() : base("name=MusicStoreDB") { }
		public DbSet<AppUser> Users { get; set; }
		public DbSet<ProductData> Products { get; set; }
		public DbSet<UserSession> UserSessions { get; set; }

		
	}
}