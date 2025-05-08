
using System.Collections.Generic;
using System.Threading.Tasks;


using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Data.DataInterfaces
{
    public interface IUserRepository
    {
        Task DeleteUserAsync(string email);
        Task AddUserAsync(AppUser user);
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(string email);
        Task<string> GetUserRoleAsync(string dataEmail);
    }
}



