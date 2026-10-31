using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Plans.GetPlanById;

public sealed record GetPlanByIdQuery(Guid Id) : IQuery<PlanResponse>;
