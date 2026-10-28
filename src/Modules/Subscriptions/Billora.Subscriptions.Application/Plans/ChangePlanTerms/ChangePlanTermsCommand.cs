using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain.Pricing;

namespace Billora.Subscriptions.Application.Plans.ChangePlanTerms;

public sealed record ChangePlanTermsCommand(
    Guid Id,
    decimal Price,
    string CurrencyCode,
    BillingUnit IntervalUnit,
    int IntervalCount,
    BillingStrategy Strategy,
    int TrialDays) : ICommand;
