using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        Task AddProduct(ProductData product);
        Task DeleteProduct(int productId);
        Task<ProductData> GetProductById(int productId);
        Task UpdateProduct(ProductData product);
        Task<List<ProductData>> GetProducts();
        Task<List<ProductData>> GetProductsByArtist(int artistId);
        Task<List<ProductData>> GetProductsByGenre(string genre);
        Task<List<ProductData>> GetProductsByBpm(int bpm);
        Task<List<ProductData>> GetProductsByScale(string scale);
    }
}