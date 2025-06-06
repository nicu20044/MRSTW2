﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Core;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic
{
    public class UserBl : AdminApi, IUser
    {
        public async Task<AppUser> GetById(int id)
        {
            return await GetByIdAsync(id);
        }

        public IEnumerable<AppUser> GetAll()
        {
            return base.GetAll();
        }

       

        public void DeleteUser(int userid)
        {
            Delete(userid);
        }


        public Task UpdateUserRole(string email, string newRole)
        {
            return UpdateUserRoleAsync(email, newRole);
        }
    }
}