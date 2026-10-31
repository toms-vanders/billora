using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Plans.GetPlans;

public sealed record GetPlansQuery(Guid TenantId) : IQuery<IReadOnlyList<PlanResponse>>;
