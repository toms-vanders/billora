using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Subscribers;

public static class SubscriberErrors
{
    public static Error NotFound(Guid id) => new(
        "Subscriber.NotFound", $"The subscriber with Id '{id}' was not found.");

    public static Error ExternalIdAlreadyExists(string externalId) => new(
        "Subscriber.ExternalIdAlreadyExists", $"The subscriber with external Id '{externalId}' already exists.");
}
