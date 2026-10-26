using Billora.SharedKernel;
using MediatR;

namespace Billora.Subscriptions.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
