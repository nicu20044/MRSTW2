using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore2.Domain.Entities.User;

using System.Linq;
using MusicStore2.BusinessLogic.Data;


namespace MusicStore.BusinessLogic.Data.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _context;

            public UserRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task DeleteUserAsync(string dataEmail)
            {
                var user = await _context.Users.FindAsync(dataEmail);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task AddUserAsync(AppUser user)
            {
                DateTime newLoginTime = DateTime.UtcNow;
                var newUser = new AppUser
                {
                    Name=user.Name,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    LastLoginTime = user.LastLoginTime,
                    UserRole = user.UserRole,
                    Id=user.Id
                    
                    
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
            {
                return await _context.Users.ToListAsync();
            }

            public async Task<AppUser> GetUserByEmailAsync(string dataEmail)
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == dataEmail);
            }
            
            public async Task<string> GetUserRoleAsync(string dataEmail)
            {
                var user = await _context.Users
                    .Where(u => u.Email == dataEmail)
                    .Select(u => u.UserRole)
                    .FirstOrDefaultAsync();

                return user ?? "Guest";
            }

            public async Task UpdateUserAsync(string email)
            {
                DateTime newLoginTime = DateTime.UtcNow;
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user != null)
                {
                    user.LastLoginTime = newLoginTime;
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
    }
}