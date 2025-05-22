using System.Threading.Tasks;
using MusicStore.BusinessLogic.Core;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic
{
    public class AuthBl : UserApi, IAuth
    {
        public async Task<UserAuthResp> LoginAction(UserLoginData data,string dataEmail)
        {
            return await UserLoginActionAsync(data, dataEmail);
        }

        public async Task<UserAuthResp> UserRegisterAction(UserRegData data, string dataEmail)
        {
            return await UserRegisterActionAsync(data, dataEmail);
        }
    }
}