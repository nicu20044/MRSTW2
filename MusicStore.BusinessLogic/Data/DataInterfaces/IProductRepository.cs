using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MusicStore2.Domain.Entities.Product;

namespace MusicStore.BusinessLogic.Data.DataInterfaces
{
	public interface IProductRepository
	{
		Task<List<ProductData>> GetProductsByArtistAsync(int artistId);
		Task<List<ProductData>> GetProductsByGenreAsync(string genre);
		Task<List<ProductData>> GetProductsByBpmAsync(int bpm);
		Task<List<ProductData>> GetProductsByScaleAsync(string scale);
		Task<List<ProductData>> GetProductsByProducerAsync(int producerId);
	}
}