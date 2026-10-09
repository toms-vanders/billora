using Billora.SharedKernel;

namespace Billora.Subscriptions.Domain.Subscriptions;

public static class SubscriptionErrors
{
    public static readonly Error AlreadyActive = new(
        "Subscription.AlreadyActive", "The subscription is already active.");

    public static readonly Error AlreadyPastDue = new(
        "Subscription.AlreadyPastDue", "The subscription is already past due.");

    public static readonly Error Terminated = new(
        "Subscription.Terminated", "The subscription has ended and cannot be changed.");

    public static readonly Error NotTrialing = new(
        "Subscription.NotTrialing", "The subscription is not in a trial.");

    public static readonly Error NotActive = new(
        "Subscription.NotActive", "The subscription is not active.");
}
