namespace Billora.Subscriptions.Application.Subscribers;

public sealed record SubscriberResponse(
    Guid Id,
    Guid TenantId,
    string ExternalId,
    string Name,
    string Email,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt);
