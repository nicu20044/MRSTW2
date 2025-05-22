using MusicStore2.Domain.Entities.Cart;
using MusicStore2.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MusicStore.BusinessLogic.Interfaces
{
	public interface ICart
	{
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int userId);
        Task AddToCartAsync(int userId, int productId);
        Task RemoveFromCartAsync(int userId, int productId);
    }
}