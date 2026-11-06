using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain;
using Billora.Subscriptions.Domain.Subscribers;

namespace Billora.Subscriptions.Application.Subscribers.CreateSubscriber;

internal sealed class CreateSubscriberCommandHandler : ICommandHandler<CreateSubscriberCommand, Guid>
{
    private readonly ISubscriberRepository _subscriberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriberCommandHandler(ISubscriberRepository subscriberRepository, IUnitOfWork unitOfWork)
    {
        _subscriberRepository = subscriberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateSubscriberCommand request, CancellationToken cancellationToken)
    {
        var externalId = new ExternalId(request.ExternalId);

        var existingSubscriber = await _subscriberRepository.GetByExternalIdAsync(
            request.TenantId, externalId, cancellationToken);

        if (existingSubscriber is not null)
            return Result.Failure<Guid>(SubscriberErrors.ExternalIdAlreadyExists(request.ExternalId));

        var subscriber = Subscriber.Create(
            request.TenantId,
            externalId,
            new SubscriberName(request.Name),
            new Email(request.Email));

        _subscriberRepository.Add(subscriber);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return subscriber.Id;
    }
}
