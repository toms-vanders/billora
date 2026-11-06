using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain;
using Billora.Subscriptions.Domain.Subscribers;

namespace Billora.Subscriptions.Application.Subscribers.ChangeSubscriberEmail;

internal sealed class ChangeSubscriberEmailCommandHandler : ICommandHandler<ChangeSubscriberEmailCommand>
{
    private readonly ISubscriberRepository _subscriberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeSubscriberEmailCommandHandler(ISubscriberRepository subscriberRepository, IUnitOfWork unitOfWork)
    {
        _subscriberRepository = subscriberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ChangeSubscriberEmailCommand request, CancellationToken cancellationToken)
    {
        var subscriber = await _subscriberRepository.GetByIdAsync(request.Id, cancellationToken);

        if (subscriber is null)
            return Result.Failure(SubscriberErrors.NotFound(request.Id));

        subscriber.ChangeEmail(new Email(request.Email));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
