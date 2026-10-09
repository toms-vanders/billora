using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Subscribers.Events;

public sealed record SubscriberCreatedDomainEvent(Guid Id) : IDomainEvent;
