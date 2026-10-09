using Billora.SharedKernel;
using Billora.Subscriptions.Domain.Pricing;
using Billora.Subscriptions.Domain.Subscriptions.Events;

namespace Billora.Subscriptions.Domain.Subscriptions;

public sealed class Subscription : Entity
{
    public Guid TenantId { get; }
    public Guid PlanId { get; }
    public Guid SubscriberId { get; }
    public PlanTerms Terms { get; }
    public int Quantity { get; }
    public DateTimeOffset StartedAt { get; }
    public int BillingAnchorDay { get; }
    public DateTimeOffset? TrialEndsAt { get; }
    public SubscriptionStatus Status { get; private set; }
    public DateRange CurrentPeriod { get; private set; }
    public bool CancelAtPeriodEnd { get; private set; }
    public DateTimeOffset? CancelRequestedAt { get; private set; }
    public DateTimeOffset? EndedAt { get; private set; }
    public DateTimeOffset? PastDueSince { get; private set; }

    private Subscription(
        Guid id,
        Guid tenantId,
        Guid planId,
        Guid subscriberId,
        PlanTerms terms,
        int quantity,
        DateTimeOffset startedAt,
        int billingAnchorDay,
        DateTimeOffset? trialEndsAt,
        SubscriptionStatus status,
        DateRange currentPeriod,
        bool cancelAtPeriodEnd,
        DateTimeOffset? cancelRequestedAt,
        DateTimeOffset? endedAt,
        DateTimeOffset? pastDueSince) : base(id)
    {
        TenantId = tenantId;
        PlanId = planId;
        SubscriberId = subscriberId;
        Terms = terms;
        Quantity = quantity;
        StartedAt = startedAt;
        BillingAnchorDay = billingAnchorDay;
        TrialEndsAt = trialEndsAt;
        Status = status;
        CurrentPeriod = currentPeriod;
        CancelAtPeriodEnd = cancelAtPeriodEnd;
        CancelRequestedAt = cancelRequestedAt;
        EndedAt = endedAt;
        PastDueSince = pastDueSince;
    }

    public static Subscription Create(
        Guid tenantId,
        Guid planId,
        Guid subscriberId,
        PlanTerms terms,
        int quantity,
        DateTimeOffset startedAt,
        bool withTrial)
    {
        if (startedAt.Offset != TimeSpan.Zero)
            throw new ArgumentException("Start date must be UTC.", nameof(startedAt));

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        var trialDays = withTrial ? terms.TrialDays : 0;
        var hasTrial = trialDays > 0;

        DateTimeOffset? trialEndsAt = hasTrial ? startedAt.AddDays(trialDays) : null;

        var billingAnchorDay = (trialEndsAt ?? startedAt).Day;

        var periodEnd = trialEndsAt ?? terms.Interval.AddTo(startedAt, billingAnchorDay);

        var status = hasTrial ? SubscriptionStatus.Trialing : SubscriptionStatus.Active;

        var subscription = new Subscription(NewId(), tenantId, planId, subscriberId, terms, quantity,
            startedAt, billingAnchorDay, trialEndsAt, status, new DateRange(startedAt, periodEnd), false, null, null, null);

        subscription.RaiseDomainEvent(new SubscriptionCreatedDomainEvent(subscription.Id));

        return subscription;
    }

    public Result MarkPastDue(DateTimeOffset utcNow)
    {
        if (Status is SubscriptionStatus.Cancelled or SubscriptionStatus.Expired)
            return Result.Failure(SubscriptionErrors.Terminated);

        if (Status is SubscriptionStatus.PastDue)
            return Result.Failure(SubscriptionErrors.AlreadyPastDue);

        Status = SubscriptionStatus.PastDue;

        PastDueSince = utcNow;

        return Result.Success();
    }

    public Result Expire(DateTimeOffset utcNow)
    {
        if (Status is SubscriptionStatus.Cancelled or SubscriptionStatus.Expired)
            return Result.Failure(SubscriptionErrors.Terminated);

        if (Status is not SubscriptionStatus.Trialing)
            return Result.Failure(SubscriptionErrors.NotTrialing);

        EndedAt = utcNow;
        Status = SubscriptionStatus.Expired;

        return Result.Success();
    }

    public Result Cancel(DateTimeOffset utcNow, bool immediately)
    {
        if (Status is SubscriptionStatus.Cancelled or SubscriptionStatus.Expired)
            return Result.Failure(SubscriptionErrors.Terminated);

        CancelRequestedAt ??= utcNow;

        if (immediately)
        {
            ApplyCancellation(utcNow);
        }
        else
        {
            CancelAtPeriodEnd = true;
        }

        return Result.Success();
    }

    public Result Renew(DateTimeOffset utcNow)
    {
        if (Status is SubscriptionStatus.Cancelled or SubscriptionStatus.Expired)
            return Result.Failure(SubscriptionErrors.Terminated);

        if (Status is not SubscriptionStatus.Active)
            return Result.Failure(SubscriptionErrors.NotActive);

        if (CancelAtPeriodEnd)
        {
            ApplyCancellation(utcNow);

            return Result.Success();
        }

        StartNewPeriod();

        return Result.Success();
    }

    public Result Activate(DateTimeOffset utcNow)
    {
        if (Status is SubscriptionStatus.Cancelled or SubscriptionStatus.Expired)
            return Result.Failure(SubscriptionErrors.Terminated);

        if (Status is SubscriptionStatus.Active)
            return Result.Failure(SubscriptionErrors.AlreadyActive);

        if (Status is SubscriptionStatus.Trialing)
        {
            if (CancelAtPeriodEnd)
            {
                ApplyCancellation(utcNow);
                return Result.Success();
            }

            StartNewPeriod();
        }

        Status = SubscriptionStatus.Active;

        return Result.Success();
    }

    public Result Resume()
    {
        if (Status is SubscriptionStatus.Cancelled or SubscriptionStatus.Expired)
            return Result.Failure(SubscriptionErrors.Terminated);

        if (CancelAtPeriodEnd)
        {
            CancelAtPeriodEnd = false;
            CancelRequestedAt = null;
        }

        return Result.Success();
    }

    private void StartNewPeriod()
    {
        var start = CurrentPeriod.End;
        var end = Terms.Interval.AddTo(start, BillingAnchorDay);

        CurrentPeriod = new DateRange(start, end);
    }

    private void ApplyCancellation(DateTimeOffset utcNow)
    {
        Status = SubscriptionStatus.Cancelled;
        EndedAt = utcNow;
        CancelAtPeriodEnd = false;

        RaiseDomainEvent(new SubscriptionCancelledDomainEvent(Id));
    }
}
