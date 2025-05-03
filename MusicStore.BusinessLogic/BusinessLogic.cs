using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicStore.BusinessLogic.Data;
using MusicStore.BusinessLogic.Data.Repositories;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore.BusinessLogic.Services;

namespace MusicStore.BusinessLogic
{
	public class BusinessLogic
	{
		public IProduct GetProductBl()
		{
			//return new ProductBL(new ProductRepository(new AppDbContext()), new AuthService(new UserRepository(new AppDbContext()), new SessionBL()));
			throw new NotImplementedException();
		}
	}
}