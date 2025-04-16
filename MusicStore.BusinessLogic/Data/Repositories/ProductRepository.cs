using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Domain.Entities.Product;


namespace MusicStore.BussinesLogic.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public override async Task<List<ProductData>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public override async Task<ProductData> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public override async Task AddAsync(ProductData entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public override async Task UpdateAsync(ProductData entity)
        {
            throw new System.NotImplementedException();
        }

        public override async Task DeleteAsync(ProductData entity)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<List<ProductData>> GetProductsByArtistAsync(int artistId)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<List<ProductData>> GetProductsByGenreAsync(string genre)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<List<ProductData>> GetProductsByBpmAsync(int bpm)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<List<ProductData>> GetProductsByScaleAsync(string scale)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<List<ProductData>> GetProductsByProducerAsync(int producerId)
        {
            throw new System.NotImplementedException();
        }
    }
}