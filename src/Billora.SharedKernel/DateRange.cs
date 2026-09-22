namespace Billora.SharedKernel;

public sealed record DateRange
{
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }

    private DateRange()
    {
    }

    public TimeSpan Duration => End - Start;

    // Start inclusive, end exclusive, so consecutive ranges don't overlap
    public bool Contains(DateTimeOffset date) => date >= Start && date < End;

    public static DateRange Create(DateTimeOffset start, DateTimeOffset end)
    {
        if (end <= start) throw new ArgumentException("End date must be after Start date.", nameof(end));

        return new DateRange
        {
            Start = start,
            End = end
        };
    }
}
