using System.Threading.Tasks;
using MusicStore.BusinessLogic.Services;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Core
{
	public class AdminApi
	{
		private readonly AuthService _authService;
        
		public AdminApi(AuthService authService)
		{
			_authService = authService;
		}

		public async Task<UserAuthResp> LoginActionAsync(UserLoginData data)
		{
			return await _authService.UserLoginActionAsync(data);
		}

		public async Task<UserAuthResp> RegisterActionAsync(UserRegData data)
		{
			return await _authService.UserRegisterActionAsync(data);
		}
	}
}