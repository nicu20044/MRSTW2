using System.Threading.Tasks;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserAuthResp> UserLoginActionAsync(UserLoginData data);
        Task<UserAuthResp> UserRegisterActionAsync(UserRegData data);
        Task<string> CreateUserSessionAsync(int userId);
    }
}