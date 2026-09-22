using Billora.SharedKernel;
using Billora.Subscriptions.Domain.Pricing;
using Billora.Subscriptions.Domain.Subscriptions.Events;

namespace Billora.Subscriptions.Domain.Subscriptions;

public sealed class Subscription : Entity
{
    public Guid TenantId { get; private set; }
    public Guid PlanId { get; private set; }
    public Guid SubscriberId { get; private set; }
    public BillingStrategy BillingStrategy { get; private set; }
    public BillingInterval BillingInterval { get; private set; }
    public SubscriptionStatus Status { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }
    public DateTimeOffset StartedAt { get; private set; }
    public int BillingAnchorDay => StartedAt.Day;
    public DateTimeOffset? TrialEndsAt { get; private set; }
    public DateRange CurrentPeriod { get; private set; }
    public bool CancelAtPeriodEnd { get; private set; }
    public DateTimeOffset? CancelledAt { get; private set; }
    public DateTimeOffset? PastDueSince { get; private set; }

    private Subscription(
        Guid id,
        Guid tenantId,
        Guid planId,
        Guid subscriberId,
        BillingStrategy billingStrategy,
        BillingInterval billingInterval,
        SubscriptionStatus status,
        Money price,
        DateTimeOffset startedAt,
        DateTimeOffset? trialEndsAt,
        DateRange currentPeriod,
        bool cancelAtPeriodEnd,
        DateTimeOffset? cancelledAt,
        DateTimeOffset? pastDueSince,
        int quantity = 1) : base(id)
    {
        TenantId = tenantId;
        PlanId = planId;
        SubscriberId = subscriberId;
        BillingStrategy = billingStrategy;
        BillingInterval = billingInterval;
        Status = status;
        Price = price;
        StartedAt = startedAt;
        Quantity = quantity;
        TrialEndsAt = trialEndsAt;
        CurrentPeriod = currentPeriod;
        CancelAtPeriodEnd = cancelAtPeriodEnd;
        CancelledAt = cancelledAt;
        PastDueSince = pastDueSince;
    }

    public static Subscription Create(Guid id,
        Guid tenantId,
        Guid planId,
        Guid subscriberId,
        BillingStrategy billingStrategy,
        BillingInterval billingInterval,
        SubscriptionStatus status,
        Money price,
        DateTimeOffset startedAt,
        DateTimeOffset? trialEndsAt,
        DateRange currentPeriod,
        bool cancelAtPeriodEnd,
        DateTimeOffset? cancelledAt,
        DateTimeOffset? pastDueSince,
        int quantity = 1)
    {
        var subscription = new Subscription(NewId(), tenantId, planId, subscriberId, billingStrategy, billingInterval,
            status, price, startedAt, trialEndsAt, currentPeriod, cancelAtPeriodEnd, cancelledAt, pastDueSince, quantity);

        subscription.RaiseDomainEvent(new SubscriptionCreatedDomainEvent(subscription.Id));

        return subscription;
    }
}
