using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Plans.Events;

public sealed record PlanCreatedDomainEvent(Guid Id) : IDomainEvent;
