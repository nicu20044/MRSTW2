using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Interfaces
{
    public interface IUser
    {
        Task<AppUser> GetById(int id);
        IEnumerable<AppUser> GetAll();
        void DeleteUser(int userid);
        Task UpdateUserRole(string email, string newRole);
    }
}