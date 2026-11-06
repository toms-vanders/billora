using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Subscribers.GetSubscribers;

public sealed record GetSubscribersQuery(Guid TenantId) : IQuery<IReadOnlyList<SubscriberResponse>>;
