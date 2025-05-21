using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Core;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.EntityBL
{
    public class UserBl : AdminApi, IUser
    {
        public async Task<AppUser> GetById(int id)
        {
            return await GetByIdAsync(id);
        }

        public IEnumerable<AppUser> GetAll()
        {
            return GetAllAsync();
        }

        public Task UpdateUser(AppUser data)
        {
            return UpdateUserAsync(data);
        }

        public Task Delete(int userid)
        {
            return DeleteAsync(userid);
        }

        public Task SaveChanges()
        {
            return Save();
        }

        

        public Task UpdateUserRole(string email, string newRole)
        {
            return  UpdateUserRoleAsync(email, newRole);
        }
    }
}