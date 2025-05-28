using MusicStore2.Domain.Entities.Subscription;

namespace MusicStore.BusinessLogic.Interfaces
{
    public interface ISubscription
    {
        void CreateSubscription(int userId, string planName, int durationDays);
        void CancelSubscription(int userId);
        bool HasActiveSubscription(int userId);
        SubscriptionData GetSubscription(int userId);
    }
}