using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Interfaces
{
	public interface IAuth
	{
		Task<UserAuthResp> LoginActionAsync(UserLoginData data);
		Task<UserAuthResp> UserRegisterActionAsync(UserRegData data);
	}
}