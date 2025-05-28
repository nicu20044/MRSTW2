using MusicStore.BusinessLogic.Core;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore2.Domain.Entities.Subscription;

namespace MusicStore.BusinessLogic
{
    public class SubscriptionBl : ArtistApi, ISubscription
    {
        public void CreateSubscription(int userId, string planName, int durationDays)
        {
            CreateSubscriptionForArtist(userId, planName, durationDays);
        }

        public void CancelSubscription(int userId)
        {
            CancelSubscriptionForArtist(userId);
        }

        public bool HasActiveSubscription(int userId)
        {
            return HasActiveSubscriptionForArtist(userId);
        }

        public SubscriptionData GetSubscription(int userId)
        {
            return GetSubscriptionForArtist(userId);
        }
    }
}