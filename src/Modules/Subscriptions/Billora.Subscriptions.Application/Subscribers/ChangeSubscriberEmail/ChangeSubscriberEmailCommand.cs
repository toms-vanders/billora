using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Subscribers.ChangeSubscriberEmail;

public sealed record ChangeSubscriberEmailCommand(
    Guid Id,
    string Email) : ICommand;
