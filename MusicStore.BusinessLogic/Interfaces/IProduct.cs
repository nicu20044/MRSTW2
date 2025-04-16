using MusicStore.Domain.Entities.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.BussinesLogic.Interfaces
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