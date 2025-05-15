using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.BusinessLogic.Data.DataInterfaces
{
	public interface IGenericRepository<T> where T : class
	{
		IEnumerable<T> GetAllAsyncFromDatabase();
		T GetById(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(int id);
		void Save();
		Task AddAsyncToDatabase(T entity);
		Task DeleteAsync(T entity);
		Task<List<T>> GetAllAsyncDatabse();
		Task<T> GetByIdAsyncFromDatabase(int id);
		Task UpdateAsyncFromDatabase(T entity);
	}
}