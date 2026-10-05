using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Subscribers;

public sealed record SubscriberName
{
    public const int MaxLength = 256;

    public string Value { get; }

    public SubscriberName(string value) => Value = StringGuard.Require(value, MaxLength);

    public override string ToString() => Value;
}
