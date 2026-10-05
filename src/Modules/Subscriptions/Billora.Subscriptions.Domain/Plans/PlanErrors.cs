using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Plans;

public static class PlanErrors
{
    public static readonly Error AlreadyInactive = new(
        "Plan.AlreadyInactive", "The plan is already inactive.");

    public static readonly Error AlreadyActive = new(
        "Plan.AlreadyActive", "The plan is already active.");
}
