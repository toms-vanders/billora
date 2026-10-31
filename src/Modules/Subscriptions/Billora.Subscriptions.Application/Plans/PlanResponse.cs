using Billora.Subscriptions.Domain.Pricing;

namespace Billora.Subscriptions.Application.Plans;

public sealed record PlanResponse(
    Guid Id,
    Guid TenantId,
    string Name,
    string? Description,
    decimal Price,
    string CurrencyCode,
    BillingUnit IntervalUnit,
    int IntervalCount,
    BillingStrategy Strategy,
    int TrialDays,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt);
