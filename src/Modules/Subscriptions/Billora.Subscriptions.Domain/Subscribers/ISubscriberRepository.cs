namespace Billora.Subscriptions.Domain.Subscribers;

public interface ISubscriberRepository
{
    Task<Subscriber?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Subscriber subscriber);
}
