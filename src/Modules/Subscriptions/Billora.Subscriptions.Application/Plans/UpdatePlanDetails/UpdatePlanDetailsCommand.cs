using Billora.Subscriptions.Application.Abstractions.Messaging;

namespace Billora.Subscriptions.Application.Plans.UpdatePlanDetails;

public sealed record UpdatePlanDetailsCommand(
    Guid Id,
    string Name,
    string? Description) : ICommand;
