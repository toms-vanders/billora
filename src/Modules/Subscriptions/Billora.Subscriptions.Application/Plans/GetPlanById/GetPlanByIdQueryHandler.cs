using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Data;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain.Plans;
using Dapper;

namespace Billora.Subscriptions.Application.Plans.GetPlanById;

internal sealed class GetPlanByIdQueryHandler : IQueryHandler<GetPlanByIdQuery, PlanResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPlanByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory) => _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<PlanResponse>> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = $"""
            {PlanSql.Select}
            WHERE id = @Id
            """;

        var plan = await connection.QuerySingleOrDefaultAsync<PlanResponse>(
            new CommandDefinition(
                sql,
                new { request.Id },
                cancellationToken: cancellationToken));

        if (plan is null)
            return Result.Failure<PlanResponse>(PlanErrors.NotFound(request.Id));

        return plan;
    }
}
