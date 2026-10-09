using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Subscriptions.Events;

public sealed record SubscriptionCancelledDomainEvent(Guid Id) : IDomainEvent;
