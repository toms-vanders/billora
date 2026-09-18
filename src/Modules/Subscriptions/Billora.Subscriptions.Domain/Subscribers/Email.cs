using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace Billora.Subscriptions.Domain.Subscribers;

[SuppressMessage("Globalization", "CA1308",
    Justification = "Emails are stored lowercase by convention, not case-normalized for security.")]
public sealed record Email
{
    public const int MaxLength = 254;   // RFC 5321 maximum

    public Email(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var normalized = value.Trim().ToLowerInvariant();

        if (normalized.Length > MaxLength)
        {
            throw new ArgumentException(
                $"Email must be at most {MaxLength} characters.", nameof(value));
        }

        if (!MailAddress.TryCreate(normalized, out var parsed) || parsed.Address != normalized)
        {
            throw new ArgumentException("Value is not a valid email address.", nameof(value));
        }

        Value = normalized;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
