using Billora.SharedKernel;
using Billora.Subscriptions.Application.Abstractions.Messaging;
using Billora.Subscriptions.Domain;
using Billora.Subscriptions.Domain.Plans;
using Billora.Subscriptions.Domain.Pricing;

namespace Billora.Subscriptions.Application.Plans.CreatePlan;

internal sealed class CreatePlanCommandHandler : ICommandHandler<CreatePlanCommand, Guid>
{
    private readonly IPlanRepository _planRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlanCommandHandler(IPlanRepository planRepository, IUnitOfWork unitOfWork)
    {
        _planRepository = planRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        var terms = new PlanTerms(
            new Money(request.Price, Currency.FromCode(request.CurrencyCode)),
            new BillingInterval(request.IntervalUnit, request.IntervalCount),
            request.Strategy,
            request.TrialDays);

        var plan = Plan.Create(
            request.TenantId,
            new PlanName(request.Name),
            string.IsNullOrWhiteSpace(request.Description) ? null : new PlanDescription(request.Description),
            terms);

        _planRepository.Add(plan);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return plan.Id;
    }
}
