using Billora.SharedKernel;
using MediatR;

namespace Billora.Subscriptions.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
