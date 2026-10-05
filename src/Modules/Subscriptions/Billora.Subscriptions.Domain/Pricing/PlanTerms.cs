using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Pricing;

public sealed record PlanTerms
{
    public Money Price { get; }
    public BillingInterval Interval { get; }
    public BillingStrategy Strategy { get; }
    public int TrialDays { get; }
    public bool HasTrial => TrialDays > 0;

    public PlanTerms(Money price, BillingInterval interval, BillingStrategy strategy, int trialDays)
    {
        if (price.Amount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price), price.Amount, "Plan price cannot be negative.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(trialDays);

        Price = price;
        Interval = interval;
        Strategy = strategy;
        TrialDays = trialDays;
    }
}
