using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Data;
using MusicStore.BusinessLogic.Services;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Domain.Entities.Subscription;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Core
{
    public class ArtistApi
    {
        private readonly AppDbContext _context = new AppDbContext();

       internal void CreateSubscriptionForArtist(int userId, string planName, int durationDays)
        {
            var existing = _context.Subscriptions.FirstOrDefault(s => s.UserId == userId);

            if (existing != null)
            {
                existing.PlanName = planName;
                existing.StartDate = DateTime.Now;
                existing.ExpiryDate = DateTime.Now.AddDays(durationDays);
                existing.IsActive = true;
            }
            else
            {
                var subscription = new SubscriptionData
                {
                    UserId = userId,
                    PlanName = planName,
                    StartDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(durationDays),
                    IsActive = true
                };
                _context.Subscriptions.Add(subscription);
            }

            _context.SaveChanges();
        }
        internal void CancelSubscriptionForArtist(int userId)
        {
            var subscription = _context.Subscriptions.FirstOrDefault(s => s.UserId == userId);
            if (subscription != null)
            {
                subscription.IsActive = false;
                subscription.ExpiryDate = DateTime.Now;
                _context.SaveChanges();
            }
        }
        internal bool HasActiveSubscriptionForArtist(int userId)
        {
            var sub = _context.Subscriptions.FirstOrDefault(s => s.UserId == userId);
            return sub != null && sub.IsActive && sub.ExpiryDate > DateTime.Now;
        }
        internal SubscriptionData GetSubscriptionForArtist(int userId)
        {
            return _context.Subscriptions.FirstOrDefault(s => s.UserId == userId);
        }
    }
}