using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Subscribers.GetSubscriberById;

public sealed record GetSubscriberByIdQuery(Guid Id) : IQuery<SubscriberResponse>;
