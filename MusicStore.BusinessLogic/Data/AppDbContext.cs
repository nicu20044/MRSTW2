using System.Data.Entity;

using MusicStore.Domain.Entities.Product;

namespace MusicStore.BussinesLogic.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=MusicStoreDB") { }
        public DbSet<ProductData> Products { get; set; }
    }
}