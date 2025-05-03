using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Data;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic.Data.Repositories
{
	public class ProductRepository:IProductRepository
	{
		private readonly AppDbContext _context;

		public ProductRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<ProductData>> GetAllAsync()
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();
		}

		public async Task<ProductData> GetByIdAsync(int id)
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();		}

		public async Task AddAsync(ProductData entity)
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();		}

		public async Task UpdateAsync(ProductData entity)
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();		}

		public async Task DeleteAsync(ProductData entity)
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();		}

		public async Task<List<ProductData>> GetProductsByArtistAsync(int artistId)
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();		}

		public async Task<List<ProductData>> GetProductsByGenreAsync(string genre)
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();		}

		public async Task<List<ProductData>> GetProductsByBpmAsync(int bpm)
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();		}

		public async Task<List<ProductData>> GetProductsByScaleAsync(string scale)
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();		}

		public async Task<List<ProductData>> GetProductsByProducerAsync(int producerId)
		{
			await Task.CompletedTask;
			throw new System.NotImplementedException();		}
	}
}