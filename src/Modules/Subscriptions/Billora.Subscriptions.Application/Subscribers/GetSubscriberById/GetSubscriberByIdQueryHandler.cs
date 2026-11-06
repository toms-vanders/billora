using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Data;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain.Subscribers;
using Dapper;

namespace Billora.Subscriptions.Application.Subscribers.GetSubscriberById;

internal sealed class GetSubscriberByIdQueryHandler : IQueryHandler<GetSubscriberByIdQuery, SubscriberResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetSubscriberByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory) => _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<SubscriberResponse>> Handle(GetSubscriberByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = $"""
            {SubscriberSql.Select}
            WHERE id = @Id
            """;

        var subscriber = await connection.QuerySingleOrDefaultAsync<SubscriberResponse>(
            new CommandDefinition(
                sql,
                new { request.Id },
                cancellationToken: cancellationToken));

        if (subscriber is null)
            return Result.Failure<SubscriberResponse>(SubscriberErrors.NotFound(request.Id));

        return subscriber;
    }
}
