﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Data;
using MusicStore.BusinessLogic.Services;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Domain.Entities.Subscription;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Core
{
    public class AdminApi
    {
        private readonly AppDbContext _context = new AppDbContext();

        internal async Task<AppUser> GetByIdAsync(int id)
        {
            var model = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

            if (model != null)
            {
                return model;
            }

            return model;
        }


        internal IEnumerable<AppUser> GetAll()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        internal async Task UpdateUserAsync(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Utilizatorul nu poate fi null.");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
          
            if (existingUser == null)
            {
                throw new InvalidOperationException("Utilizatorul nu a fost găsit.");
            }


            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.UserRole = user.UserRole;

            _context.Entry(existingUser).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserRoleAsync(string email, string newRole)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email-ul nu poate fi gol.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new InvalidOperationException("Utilizatorul nu a fost găsit.");

            user.UserRole = newRole;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        internal void Delete(int userId)
        {
            var entity = _context.Users.FirstOrDefault(p => p.Id == userId);
            if (entity == null)
            {
                throw new ArgumentException("User not found");
            }

            _context.Users.Remove(entity);
            _context.SaveChanges();
        }
        
        internal List<PlanData> GetAllPlans()
        {
            return _context.Plans.ToList();
        }
        
        internal PlanData GetPlanById(int id)
        {
            return _context.Plans.Find(id);
        }
        
        internal void CreatePlan(PlanData plan)
        {
            _context.Plans.Add(plan);
            _context.SaveChanges();
        }
        
        internal void UpdatePlan(PlanData plan)
        {
            var existingPlan = _context.Plans.Find(plan.Id);
            if (existingPlan != null)
            {
                existingPlan.Name = plan.Name;
                existingPlan.Price = plan.Price;
                existingPlan.DurationDays = plan.DurationDays;

                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Plan not found");
            }
        }


        internal void DeletePlan(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Plan not found");
            }
        }

        
    }
}