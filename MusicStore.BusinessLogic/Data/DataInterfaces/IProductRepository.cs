using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Domain.Entities.Product;

namespace MusicStore.BussinesLogic.Data
{
    public abstract class IProductRepository : IGenericRepository<ProductData>
    {
        public abstract Task<List<ProductData>> GetProductsByArtistAsync(int artistId);
        public abstract Task<List<ProductData>> GetProductsByGenreAsync(string genre);
        public abstract Task<List<ProductData>> GetProductsByBpmAsync(int bpm);
        public abstract Task<List<ProductData>> GetProductsByScaleAsync(string scale);
        public abstract Task<List<ProductData>> GetProductsByProducerAsync(int producerId);
        public abstract Task AddAsync(ProductData entity);
        public abstract Task DeleteAsync(ProductData entity);
        public abstract Task<List<ProductData>> GetAllAsync();
        public abstract Task<ProductData> GetByIdAsync(int id);
        public abstract Task UpdateAsync(ProductData entity);
    }
}