// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Web;
// using MusicStore2.BusinessLogic.Data.DataInterfaces;
// using MusicStore2.BusinessLogic.Services;
// using MusicStore2.Domain.Entities.Product;
// using MusicStore2.Domain.Entities.User;
//
// namespace MusicStore.BusinessLogic.Core
// {
// 	public class ArtistApi
// 	{
// 		private readonly IProductRepository _productRepository;
//
//         public ArtistApi(IProductRepository productRepository,AuthService authService)
//         {
//             _productRepository = productRepository;
//             _authService = authService;
//         }
//
//         //-----------------------Arist beat actions--------------------
//         internal async Task AddProduct(ProductData product)
//         {
//             if (product == null)
//             {
//                 throw new ArgumentException("Product cannot be null");
//             }
//
//             await _productRepository.AddAsync(product);
//         }
//
//         internal async Task DeleteProduct(int productId)
//         {
//             var entity = await _productRepository.GetByIdAsync(productId);
//             if (entity == null)
//             {
//                 throw new ArgumentException("Product not found");
//             }
//
//             await _productRepository.DeleteAsync(entity);
//         }
//
//         internal async Task<ProductData> GetProductById(int productId)
//         {
//             if (productId < 0)
//             {
//                 throw new ArgumentException("Product Id cannot be less than 0");
//             }
//
//             return await _productRepository.GetByIdAsync(productId);
//         }
//
//         internal async Task UpdateProduct(ProductData product)
//         {
//             if (product == null)
//             {
//                 throw new ArgumentException("Product cannot be null");
//             }
//
//             var productToUpdate = await _productRepository.GetByIdAsync(product.Id);
//
//             if (productToUpdate != null)
//             {
//                 productToUpdate.Name = product.Name;
//                 productToUpdate.Price = product.Price;
//                 productToUpdate.ArtistId = product.ArtistId;
//                 productToUpdate.Genre = product.Genre;
//                 productToUpdate.ProducerId = product.ProducerId;
//                 productToUpdate.Bpm = product.Bpm;
//             }
//
//             await _productRepository.UpdateAsync(productToUpdate);
//         }
//
//         internal async Task<List<ProductData>> GetProducts()
//         {
//             var products = await _productRepository.GetAllAsync();
//             return products.ToList();
//         }
//
//         internal async Task<List<ProductData>> GetProductsByArtist(int artistId)
//         {
//             if (artistId < 0)
//             {
//                 throw new ArgumentException("Artist Id cannot be less than 0");
//             }
//
//             return await _productRepository.GetProductsByArtistAsync(artistId);
//         }
//
//         internal async Task<List<ProductData>> GetProductsByGenre(string genre)
//         {
//             if (string.IsNullOrWhiteSpace(genre))
//             {
//                 throw new ArgumentException("Scale cannot be empty");
//             }
//             return await _productRepository.GetProductsByGenreAsync(genre);
//         }
//
//         internal async Task<List<ProductData>> GetProductsByProducer(int producerId)
//         {
//             if (producerId < 0)
//             {
//                 throw new ArgumentException("Producer Id cannot be less than 0");
//             }
//             return await _productRepository.GetProductsByProducerAsync(producerId);
//         }
//
//         internal async Task<List<ProductData>> GetProductsByBpm(int bpm)
//         {
//             if (bpm < 0)
//             {
//                 throw new ArgumentException("BPM cannot be less than  0");
//             }
//             return await _productRepository.GetProductsByBpmAsync(bpm);
//         }
//
//         internal async Task<List<ProductData>> GetProductsByScale(string scale)
//         {
//             if (string.IsNullOrWhiteSpace(scale))
//             {
//                 throw new ArgumentException("Scale cannot be empty");
//             }
//             return await _productRepository.GetProductsByScaleAsync(scale);
//         }
//         
//         
//         //-----------------------Artist Authentication--------------------
//         private readonly AuthService _authService;
//         
//         public async Task<UserAuthResp> LoginActionAsync(UserLoginData data)
//         {
//             return await _authService.UserLoginActionAsync(data);
//         }
//         public async Task<UserAuthResp> RegisterActionAsync(UserRegData data)
//         {
//             return await _authService.UserRegisterActionAsync(data);
//         }
// 	}
// }