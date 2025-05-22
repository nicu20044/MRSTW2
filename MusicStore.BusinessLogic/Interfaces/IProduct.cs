using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        Task<ProductData> GetById(int id);
        IEnumerable<ProductData> GetAll();
        Task Create(ProductData productData);
        Task Update(ProductData productData);
<<<<<<< Updated upstream
        void Delete(int productId);
        Task<IEnumerable<ProductData>> SearchByNameAsync(string query);
=======
        Task Delete(int productId);


        Task<IEnumerable<ProductData>> SearchByNameOrArtistAsync(string query);

>>>>>>> Stashed changes
    }
}