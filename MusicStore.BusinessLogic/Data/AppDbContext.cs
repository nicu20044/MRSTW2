using System.Data.Entity;
using System.Linq;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Domain.Entities.Subscription;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext() : base("name=MusicStoreDB") { }
		public DbSet<AppUser> Users { get; set; }
		public DbSet<ProductData> Products { get; set; }
		public DbSet<UserSession> UserSessions { get; set; }
		public DbSet<UserCartItem> UserCartItems { get; set; }
		public DbSet<PlanData> Plans { get; set; }
        public DbSet<SubscriptionData> Subscriptions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
	        modelBuilder.Entity<SubscriptionData>()
		        .HasRequired(s => s.User)
		        .WithOptional(u => u.Subscription);

	        base.OnModelCreating(modelBuilder);
        }
	}
}