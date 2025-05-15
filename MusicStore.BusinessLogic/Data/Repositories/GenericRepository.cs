
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Data.DataInterfaces;

// using MusicStore2.Models;
namespace MusicStore.BusinessLogic.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAllAsyncFromDatabase()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task AddAsyncToDatabase(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsyncDatabse()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsyncFromDatabase(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsyncFromDatabase(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

