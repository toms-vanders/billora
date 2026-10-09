using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Plans;

public sealed record PlanName
{
    public const int MaxLength = 100;

    public string Value { get; }

    public PlanName(string value) => Value = StringGuard.Require(value, MaxLength);

    public override string ToString() => Value;
}
