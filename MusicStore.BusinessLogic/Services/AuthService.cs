using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISession _session;

        public AuthService(IUserRepository userRepository, ISession session)
        {
            _userRepository = userRepository;
            _session = session;
        }

        public async Task<UserAuthResp> UserLoginActionAsync(UserLoginData data)
        {
            throw new NotImplementedException();
        }
        public async Task<UserAuthResp> UserRegisterActionAsync(UserRegData data)
         {
             throw new NotImplementedException();

         }

        private string ComputeHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

    }
}


