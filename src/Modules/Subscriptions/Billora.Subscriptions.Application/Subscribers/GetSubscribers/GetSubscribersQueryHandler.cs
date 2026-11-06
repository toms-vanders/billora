using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Data;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Dapper;

namespace Billora.Subscriptions.Application.Subscribers.GetSubscribers;

internal sealed class GetSubscribersQueryHandler : IQueryHandler<GetSubscribersQuery, IReadOnlyList<SubscriberResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetSubscribersQueryHandler(ISqlConnectionFactory sqlConnectionFactory) => _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<SubscriberResponse>>> Handle(GetSubscribersQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = $"""
            {SubscriberSql.Select}
            WHERE tenant_id = @TenantId
            ORDER BY created_at DESC
            """;

        var subscribers = await connection.QueryAsync<SubscriberResponse>(
            new CommandDefinition(
                sql,
                new { request.TenantId },
                cancellationToken: cancellationToken));

        return subscribers.ToList();
    }
}
