using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Plans;

public sealed record PlanDescription
{
    public const int MaxLength = 1000;

    public string Value { get; }

    public PlanDescription(string value) => Value = StringGuard.Require(value, MaxLength);

    public override string ToString() => Value;
}
