namespace Billora.Subscriptions.Domain.Subscribers;

public sealed record ExternalId
{
    public const int MaxLength = 128;

    public ExternalId(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength) throw new ArgumentException($"External id must be at most {MaxLength} characters.",
            nameof(value));

        Value = trimmed;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
