using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Core;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.Cart;

namespace MusicStore.BusinessLogic
{
        public class CartBl : UserApi, ICart
        {
            public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int userId)
            {
                return await GetUserCartItemsAsync(userId);
            }

            public async Task AddToCartAsync(int userId, int productId)
            {
                await AddItemToCartAsync(userId, productId);
            }

            public async Task RemoveFromCartAsync(int userId, int productId)
            {
                await RemoveItemFromCartAsync(userId, productId);
            }
        }
    }
