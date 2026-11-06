using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Subscribers.CreateSubscriber;

public sealed record CreateSubscriberCommand(
    Guid TenantId,
    string ExternalId,
    string Name,
    string Email) : ICommand<Guid>;
