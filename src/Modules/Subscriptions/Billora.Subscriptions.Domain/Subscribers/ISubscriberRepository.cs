namespace Billora.Subscriptions.Domain.Subscribers;

public interface ISubscriberRepository
{
    Task<Subscriber?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Subscriber?> GetByExternalIdAsync(Guid tenantId, ExternalId externalId, CancellationToken cancellationToken = default);

    void Add(Subscriber subscriber);
}
