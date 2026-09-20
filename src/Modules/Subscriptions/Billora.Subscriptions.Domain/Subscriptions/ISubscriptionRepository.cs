namespace Billora.Subscriptions.Domain.Subscriptions;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

}
