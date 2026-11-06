using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Subscribers.RenameSubscriber;

public sealed record RenameSubscriberCommand(
    Guid Id,
    string Name) : ICommand;
