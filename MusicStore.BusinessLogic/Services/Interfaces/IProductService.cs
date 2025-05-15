using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductData> GetByIdAsync(int id);
        Task<List<ProductData>> GetAllAsync();
        Task CreateAsync(ProductData productData);
        Task UpdateAsync(ProductData productData);
        Task DeleteAsync(int id);
    }
}