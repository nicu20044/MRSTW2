using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic.Data.DataInterfaces
{
    public interface IProductRepository : IGenericRepository<ProductData>
    {
        Task<List<ProductData>> GetProductsByArtistAsync(int artistId);
        Task<List<ProductData>> GetProductsByGenreAsync(string genre);
        Task<List<ProductData>> GetProductsByBpmAsync(int bpm);
        Task<List<ProductData>> GetProductsByScaleAsync(string scale);
        Task<List<ProductData>> GetProductsByProducerAsync(int producerId);
    }
}