using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Subscribers;

public sealed record ExternalId
{
    public const int MaxLength = 128;

    public ExternalId(string value) => Value = StringGuard.Require(value, MaxLength);

    public string Value { get; }

    public override string ToString() => Value;
}
