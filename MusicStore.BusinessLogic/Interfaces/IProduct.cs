using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        Task<ProductData> GetById(int id);
        Task<List<ProductData>> GetAll();
        Task Create(ProductData productData);
        Task Update(ProductData productData, int id);
        Task Delete(int productId);
    }
}