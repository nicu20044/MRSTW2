using MusicStore.BusinessLogic.Interfaces;
using MusicStore.BusinessLogic.Services;

namespace MusicStore.BusinessLogic
{
    public class BusinessLogic
    {

        public IProduct GetProductBl()
        {
            return new ProductBl();
        }

        public IAuth GetAuthBl()
        {
            return new AuthBl();
        }

        public ISession GetSessionBl()
        {
            return new SessionBl();
        }

        public IUser GetUserBl()
        {
            return new UserBl();
        }

        public ICart GetCartBl()
        {
            return new CartBl();
        }
    }
}