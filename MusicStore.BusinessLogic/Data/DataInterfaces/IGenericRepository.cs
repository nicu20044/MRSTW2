using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MusicStore2.BusinessLogic.Data.DataInterfaces
{
	public interface IGenericRepository<T> where T : class
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		void Insert(T entity);
		void Update(T entity);
		void Delete(int id);
		void Save();
	}
}