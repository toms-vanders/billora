using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Subscribers.Events;

public sealed record SubscriberEmailChangedDomainEvent(
    Guid SubscriberId,
    string OldEmail,
    string NewEmail) : IDomainEvent;
