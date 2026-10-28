using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain;
using Billora.Subscriptions.Domain.Plans;

namespace Billora.Subscriptions.Application.Plans.UpdatePlanDetails;

internal sealed class UpdatePlanDetailsCommandHandler : ICommandHandler<UpdatePlanDetailsCommand>
{
    private readonly IPlanRepository _planRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePlanDetailsCommandHandler(IPlanRepository planRepository, IUnitOfWork unitOfWork)
    {
        _planRepository = planRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdatePlanDetailsCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);

        if (plan is null)
        {
            return Result.Failure(PlanErrors.NotFound(request.Id));
        }

        plan.UpdateDetails(
            new PlanName(request.Name),
            string.IsNullOrWhiteSpace(request.Description) ? null : new PlanDescription(request.Description));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
