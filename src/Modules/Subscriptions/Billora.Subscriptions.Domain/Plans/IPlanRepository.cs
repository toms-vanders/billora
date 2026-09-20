namespace Billora.Subscriptions.Domain.Plans;

public interface IPlanRepository
{
    Task<Plan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

}
