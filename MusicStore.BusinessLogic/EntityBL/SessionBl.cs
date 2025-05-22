using System.Threading.Tasks;
using MusicStore.BusinessLogic.Core;
using MusicStore.BusinessLogic.Interfaces;

namespace MusicStore.BusinessLogic
{
    public class SessionBl : UserApi, ISession
    {
        public async Task<string> CreateUserSession(int userId)
        {
            return await CreateUserSessionAsync(userId);
        }
    }
}