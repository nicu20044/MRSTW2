using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore.BusinessLogic.Services.Interfaces;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductData> GetByIdAsync(int id)
        {
            var model = await _productRepository.GetByIdAsyncFromDatabase(id);

            if (model != null)
            {
                return model;
            }

            return model;
        }

        public async Task<List<ProductData>> GetAllAsync()
        {
            var models = await _productRepository.GetAllAsyncDatabse();
            return models;
        }

        public async Task CreateAsync(ProductData productData)
        {
            if (productData == null)
            {
                throw new ArgumentException("Product cannot be null");
            }
            

            await _productRepository.AddAsyncToDatabase(productData);
        }

        public async Task UpdateAsync(ProductData productData)
        {
            if (productData == null)
            {
                throw new ArgumentException("Product cannot be null");
            }

            var productToUpdate = await _productRepository.GetByIdAsyncFromDatabase(productData.Id);

            if (productToUpdate != null)
            {
                productToUpdate.Name = productData.Name;
                productToUpdate.Price = productData.Price;
                productToUpdate.ArtistId = productData.ArtistId;
                productToUpdate.Genre = productData.Genre;
                productToUpdate.ProducerId = productData.ProducerId;
                productToUpdate.Bpm = productData.Bpm;
            }

            await _productRepository.UpdateAsyncFromDatabase(productToUpdate);
        }

        public async Task DeleteAsync(int productId)
        {
            var entity = await _productRepository.GetByIdAsyncFromDatabase(productId);
            if (entity == null)
            {
                throw new ArgumentException("Product not found");
            }

            await _productRepository.DeleteAsync(entity);
        }
    }
    
}
