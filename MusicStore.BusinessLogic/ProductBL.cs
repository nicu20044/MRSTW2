using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.BussinesLogic.Core.UserArtist;
using MusicStore.BussinesLogic.Data;
using MusicStore.BussinesLogic.Interfaces;
using MusicStore.Domain.Entities.Product;

namespace MusicStore.BussinesLogic
{
    public class ProductBL : ArtistApi, IProduct
    {

        public ProductBL(IProductRepository productRepository) : base(productRepository)
        {
        }

        public new async Task AddProduct(ProductData product)
        {
            await base.AddProduct(product);
        }

        public new async Task DeleteProduct(int productId)
        {
            await base.DeleteProduct(productId);
        }

        public new async Task<ProductData> GetProductById(int productId)
        {
            if (productId < 0)
            {
                throw new System.ArgumentException("Product Id cannot be less than 0");
            }
            return await base.GetProductById(productId);
        }

        public new async Task UpdateProduct(ProductData product)
        {
            await base.UpdateProduct(product);
        }

        public new async Task<List<ProductData>> GetProducts()
        {
            return await base.GetProducts();
        }

        public new async Task<List<ProductData>> GetProductsByArtist(int artistId)
        {
            if (artistId < 0)
            {
                throw new System.ArgumentException("Artist Id cannot be less than 0");
            }
            return await base.GetProductsByArtist(artistId);
        }

        public new async Task<List<ProductData>> GetProductsByGenre(string genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
            {
                throw new System.ArgumentException("Scale cannot be empty");
            }
            return await base.GetProductsByGenre(genre);
        }

        public new async Task<List<ProductData>> GetProductsByBpm(int bpm)
        {
            if (bpm < 0)
            {
                throw new System.ArgumentException("BPM cannot be less than  0");
            }
            return await base.GetProductsByBpm(bpm);
        }

        public new async Task<List<ProductData>> GetProductsByScale(string scale)
        {
            if (string.IsNullOrWhiteSpace(scale))
            {
                throw new System.ArgumentException("Scale cannot be empty");
            }
            return await base.GetProductsByScale(scale);
        }
    }
}