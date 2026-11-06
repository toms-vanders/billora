namespace Billora.Subscriptions.Application.Subscribers;

internal static class SubscriberSql
{
    /// <summary>
    /// Shared projection for every subscriber read. Column order must track
    /// <see cref="SubscriberResponse"/>'s constructor parameters — Dapper matches them by position
    /// and name, and throws if they diverge.
    /// </summary>
    internal const string Select = """
        SELECT id, tenant_id, external_id, name, email, created_at, updated_at
        FROM subscriptions.subscribers
        """;
}
