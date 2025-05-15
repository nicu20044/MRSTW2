using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductData>> GetAllAsyncDatabse()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductData> GetByIdAsyncFromDatabase(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddAsyncToDatabase(ProductData entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsyncFromDatabase(ProductData entity)
        {
            var dbo = await _context.Products.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (dbo == null)
            {
                return;
            }

            dbo.Bpm = entity.Bpm;
            // ...

            await _context.SaveChangesAsync();
        }

        public IEnumerable<ProductData> GetAllAsyncFromDatabase()
        {
            return _context.Products.AsNoTracking().ToList();
        }

        public ProductData GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ProductData entity)
        {
            _context.Products.Add(entity);
        }

        public void Update(ProductData entity)
        {
            var dbo = _context.Products.FirstOrDefault(x => x.Id == entity.Id);

            if (dbo == null)
            {
                return;
            }

            dbo.Bpm = entity.Bpm;
            // ...

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(ProductData entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ProductData>> GetProductsByArtistAsync(int artistId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ProductData>> GetProductsByGenreAsync(string genre)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ProductData>> GetProductsByBpmAsync(int bpm)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ProductData>> GetProductsByScaleAsync(string scale)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ProductData>> GetProductsByProducerAsync(int producerId)
        {
            throw new System.NotImplementedException();
        }
    }
}