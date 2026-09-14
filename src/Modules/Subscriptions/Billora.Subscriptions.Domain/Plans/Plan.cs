using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Plans
{
    public sealed class Plan : Entity
    {
        public Guid TenantId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public BillingStrategy BillingStrategy { get; private set; }
        public BillingInterval BillingInterval { get; private set; }
        public int IntervalCount { get; private set; }
        public int TrialDays { get; private set; }
        public bool IsActive { get; private set; }

        public Plan(Guid id) : base(id)
        {
        }
    }
}
