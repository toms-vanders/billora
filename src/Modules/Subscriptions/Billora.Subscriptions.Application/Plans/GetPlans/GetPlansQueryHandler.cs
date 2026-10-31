using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Data;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Dapper;

namespace Billora.Subscriptions.Application.Plans.GetPlans;

internal sealed class GetPlansQueryHandler : IQueryHandler<GetPlansQuery, IReadOnlyList<PlanResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPlansQueryHandler(ISqlConnectionFactory sqlConnectionFactory) => _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<PlanResponse>>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = $"""
            {PlanSql.Select}
            WHERE tenant_id = @TenantId
            ORDER BY created_at DESC
            """;

        var plans = await connection.QueryAsync<PlanResponse>(
            new CommandDefinition(
                sql,
                new { request.TenantId },
                cancellationToken: cancellationToken));

        return plans.ToList();
    }
}
