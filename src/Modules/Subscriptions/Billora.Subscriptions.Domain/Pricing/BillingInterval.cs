namespace Billora.Subscriptions.Domain.Pricing;

public sealed record BillingInterval(BillingUnit Unit, int Count)
{
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


