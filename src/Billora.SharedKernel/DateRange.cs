namespace Billora.SharedKernel;

public sealed record DateRange
{
    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }

    public DateRange(DateTimeOffset start, DateTimeOffset end)
    {
        if (end <= start) throw new ArgumentException("End date must be after Start date.", nameof(end));

        Start = start;
        End = end;
    }

    public TimeSpan Duration => End - Start;

    // Start inclusive, end exclusive, so consecutive ranges don't overlap
    public bool Contains(DateTimeOffset date) => date >= Start && date < End;
}
