using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MusicStore.BusinessLogic.Data;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<string> GetUserRoleAsync(string email)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
                
            return user?.UserRole ?? "Listener";
        }

        public async Task UpdateUserAsync(string email)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
                
            if (user != null)
            {
                user.LastLoginTime = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddUserAsync(AppUser user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string email)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
                
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}