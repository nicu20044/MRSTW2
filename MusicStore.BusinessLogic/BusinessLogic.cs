using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicStore.BussinesLogic;
using MusicStore.BussinesLogic.Data;
using MusicStore.BussinesLogic.Data.Repositories;
using MusicStore.BussinesLogic.Interfaces;

namespace MusicStore.BusinessLogic
{
	public class BusinessLogic
	{
		public IProduct GetProductBl()
		{
			return new ProductBL(new ProductRepository(new AppDbContext()));
		}
	}
}