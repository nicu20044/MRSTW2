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
		Task<UserAuthResp> LoginAction(UserLoginData data, string dataEmail);
		Task<UserAuthResp> UserRegisterAction(UserRegData data, string dataEmail);
		
		Task<UserAuthResp> ChangeUserPassword(string email, string newPassword);	}
}