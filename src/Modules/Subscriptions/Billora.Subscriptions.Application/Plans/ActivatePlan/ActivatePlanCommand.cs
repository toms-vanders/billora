using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Plans.ActivatePlan;

public sealed record ActivatePlanCommand(Guid Id) : ICommand;
