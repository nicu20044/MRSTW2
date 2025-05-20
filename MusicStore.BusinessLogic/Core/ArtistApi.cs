using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore.BusinessLogic.Services;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Core
{
    public class ArtistApi
    {
        private readonly IProductRepository _productRepository;

        public ArtistApi(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        

    }
}