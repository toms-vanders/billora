namespace Billora.Subscriptions.Domain.Subscribers;

public sealed record SubscriberName
{
    public const int MaxLength = 256;

    public SubscriberName(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength)
        {
            throw new ArgumentException(
                $"Name must be at most {MaxLength} characters.", nameof(value));
        }

        Value = trimmed;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
