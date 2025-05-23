﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Core;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic
{
    public class ProductBl : UserApi, IProduct
    {
        public async Task<ProductData> GetById(int id)
        {
            return await GetByIdAsync(id);
        }

        public IEnumerable<ProductData> GetAll()
        {
            return GetAllAsync();
        }

        public Task Create(ProductData productData)
        {
            return CreateAsync(productData);
        }

        public Task Update(ProductData productData)
        {
            return UpdateProductAsync(productData);
        }

        public void Delete(int productId)
        {
            DeleteProductById(productId);
        }


        public async Task<IEnumerable<ProductData>> SearchByNameOrArtistAsync(string query)
        {
            return await base.SearchByNameOrArtistAsync(query);
        }

    }
}