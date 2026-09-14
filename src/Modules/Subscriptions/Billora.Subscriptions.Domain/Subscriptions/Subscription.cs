using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Subscriptions
{
    public sealed class Subscription : Entity
    {
        public Guid TenantId { get; private set; }
        public Guid PlanId { get; private set; }
        public Guid SubscriberId { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public int Quantity { get; private set; } = 1;
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public DateTimeOffset? TrialEndsAt { get; private set; }
        public DateTimeOffset CurrentPeriodStart { get; private set; }
        public DateTimeOffset CurrentPeriodEnd { get; private set; }
        public bool CancelAtPeriodEnd { get; private set; }
        public DateTimeOffset? CancelledAt { get; private set; }
        public DateTimeOffset? PastDueSince { get; private set; }


        public Subscription(Guid id) : base(id)
        {
        }
    }
}
