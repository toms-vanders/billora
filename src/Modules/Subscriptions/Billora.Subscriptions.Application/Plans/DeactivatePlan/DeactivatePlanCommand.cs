using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Plans.DeactivatePlan;

public sealed record DeactivatePlanCommand(Guid Id) : ICommand;
