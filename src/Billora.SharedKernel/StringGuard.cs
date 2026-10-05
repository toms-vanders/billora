using System.Runtime.CompilerServices;

namespace Billora.SharedKernel;

public static class StringGuard
{
    public static string Require(
        string? value,
        int maxLength,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName);

        var trimmed = value.Trim();

        ArgumentOutOfRangeException.ThrowIfGreaterThan(trimmed.Length, maxLength, paramName);

        return trimmed;
    }
}
