using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.User;
using MusicStore.BusinessLogic.Data;
using MusicStore.Domain.Entities;
using MusicStore.BussinesLogic.Data;

namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
    
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    
        public async Task<AppUser> GetUserByIdAsync(int userId) => await _userRepository.GetByIdAsync(userId);
        public async Task RegisterUserAsync(AppUser user) => await _userRepository.AddAsync(user);
    }
}