using Billora.SharedKernel;
using Billora.Subscriptions.Domain.Pricing;

namespace Billora.Subscriptions.Domain.Subscriptions;

public sealed class Subscription : Entity
{
    public Guid TenantId { get; private set; }
    public Guid PlanId { get; private set; }
    public Guid SubscriberId { get; private set; }
    public BillingStrategy BillingStrategy { get; private set; }
    public BillingInterval BillingInterval { get; private set; }
    public SubscriptionStatus Status { get; private set; }
    public int Quantity { get; private set; } = 1;
    public Money Price { get; private set; }
    public DateTimeOffset StartedAt { get; private set; }
    public int BillingAnchorDay => StartedAt.Day;
    public DateTimeOffset? TrialEndsAt { get; private set; }
    public DateRange CurrentPeriod { get; private set; }
    public bool CancelAtPeriodEnd { get; private set; }
    public DateTimeOffset? CancelledAt { get; private set; }
    public DateTimeOffset? PastDueSince { get; private set; }


    public Subscription(Guid id) : base(id)
    {
    }
}
