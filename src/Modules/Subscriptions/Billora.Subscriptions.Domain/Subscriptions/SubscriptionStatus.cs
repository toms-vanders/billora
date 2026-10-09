namespace Billora.Subscriptions.Domain.Subscriptions;

public enum SubscriptionStatus
{
    Trialing = 1,
    Active = 2,
    PastDue = 3,
    Cancelled = 4,
    Expired = 5,
}
