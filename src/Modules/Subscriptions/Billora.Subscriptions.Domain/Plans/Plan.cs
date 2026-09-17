using Billora.SharedKernel;
using Billora.Subscriptions.Domain.Pricing;

namespace Billora.Subscriptions.Domain.Plans;

public sealed class Plan : Entity
{
    public Guid TenantId { get; private set; }
    public PlanName Name { get; private set; }
    public PlanDescription? Description { get; private set; }
    public Money Price { get; private set; }
    public BillingStrategy BillingStrategy { get; private set; }
    public BillingInterval BillingInterval { get; private set; }
    public int TrialDays { get; private set; }
    public bool IsActive { get; private set; }

    public Plan(Guid id) : base(id)
    {
    }
}
