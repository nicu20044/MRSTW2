using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Interfaces
{
	public interface ISession
	{
		Task<UserAuthResp> UserLogin(UserLoginData data);
	}
}