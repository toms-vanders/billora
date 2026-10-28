using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain.Pricing;

namespace Billora.Subscriptions.Application.Plans.CreatePlan;

public sealed record CreatePlanCommand(
    Guid TenantId,
    string Name,
    string? Description,
    decimal Price,
    string CurrencyCode,
    BillingUnit IntervalUnit,
    int IntervalCount,
    BillingStrategy Strategy,
    int TrialDays) : ICommand<Guid>;
