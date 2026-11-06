namespace Billora.Subscriptions.Application.Plans;

internal static class PlanSql
{
    /// <summary>
    /// Shared projection for every plan read. Column order must track <see cref="PlanResponse"/>'s
    /// constructor parameters — Dapper matches them by position and name, and throws if they diverge.
    /// </summary>
    internal const string Select = """
        SELECT id, tenant_id, name, description, price, currency_code,
               interval_unit, interval_count, strategy, trial_days,
               is_active, created_at, updated_at
        FROM subscriptions.plans
        """;
}
