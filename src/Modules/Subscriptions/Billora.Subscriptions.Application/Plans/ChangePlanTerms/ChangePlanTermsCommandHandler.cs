using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain;
using Billora.Subscriptions.Domain.Plans;
using Billora.Subscriptions.Domain.Pricing;

namespace Billora.Subscriptions.Application.Plans.ChangePlanTerms;

internal sealed class ChangePlanTermsCommandHandler : ICommandHandler<ChangePlanTermsCommand>
{
    private readonly IPlanRepository _planRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePlanTermsCommandHandler(IPlanRepository planRepository, IUnitOfWork unitOfWork)
    {
        _planRepository = planRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ChangePlanTermsCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);

        if (plan is null)
            return Result.Failure(PlanErrors.NotFound(request.Id));

        plan.ChangeTerms(new PlanTerms(
            new Money(request.Price, Currency.FromCode(request.CurrencyCode)),
            new BillingInterval(request.IntervalUnit, request.IntervalCount),
            request.Strategy,
            request.TrialDays));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
