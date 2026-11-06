using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain;
using Billora.Subscriptions.Domain.Subscribers;

namespace Billora.Subscriptions.Application.Subscribers.RenameSubscriber;

internal sealed class RenameSubscriberCommandHandler : ICommandHandler<RenameSubscriberCommand>
{
    private readonly ISubscriberRepository _subscriberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RenameSubscriberCommandHandler(ISubscriberRepository subscriberRepository, IUnitOfWork unitOfWork)
    {
        _subscriberRepository = subscriberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RenameSubscriberCommand request, CancellationToken cancellationToken)
    {
        var subscriber = await _subscriberRepository.GetByIdAsync(request.Id, cancellationToken);

        if (subscriber is null)
            return Result.Failure(SubscriberErrors.NotFound(request.Id));

        subscriber.Rename(new SubscriberName(request.Name));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
