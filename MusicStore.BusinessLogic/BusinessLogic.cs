using MusicStore.BusinessLogic.EntityBL.EntityBL;
using MusicStore.BusinessLogic.Interfaces;

namespace MusicStore.BusinessLogic.EntityBL
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
    }
}