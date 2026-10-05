namespace Billora.Subscriptions.Domain.Pricing;

public sealed record BillingInterval
{
    public BillingUnit Unit { get; }
    public int Count { get; }

    public BillingInterval(BillingUnit unit, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        Unit = unit;
        Count = count;
    }

    /// <summary>
    /// Advances <paramref name="date"/> by one interval, re-expanding to
    /// <paramref name="billingAnchorDay"/> wherever the target month has room for it.
    /// A subscriber anchored on the 31st lands on Feb 28, then Mar 31 — not Mar 28 —
    /// so chaining from each period's end stays on the anchor instead of drifting.
    /// </summary>
    public DateTimeOffset AddTo(DateTimeOffset date, int billingAnchorDay) => Unit switch
    {
        BillingUnit.Day => date.AddDays(Count),
        BillingUnit.Week => date.AddDays(7 * Count),
        BillingUnit.Month => AddMonths(date, Count, billingAnchorDay),
        BillingUnit.Year => AddMonths(date, Count * 12, billingAnchorDay),
        _ => throw new InvalidOperationException($"Unsupported billing unit: {Unit}"),
    };

    private static DateTimeOffset AddMonths(DateTimeOffset date, int months, int anchorDay)
    {
        var target = date.AddMonths(months);

        var day = Math.Min(anchorDay, DateTime.DaysInMonth(target.Year, target.Month));

        return new DateTimeOffset(
            target.Year,
            target.Month,
            day,
            date.Hour,
            date.Minute,
            date.Second,
            date.Millisecond,
            date.Offset);
    }
}
