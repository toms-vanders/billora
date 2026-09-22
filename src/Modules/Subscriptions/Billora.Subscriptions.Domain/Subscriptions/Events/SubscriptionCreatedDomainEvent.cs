using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Subscriptions.Events;

public sealed record SubscriptionCreatedDomainEvent(Guid Id) : IDomainEvent;
