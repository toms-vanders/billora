using Billora.SharedKernel;
using Billora.Subscriptions.Domain.Plans.Events;
using Billora.Subscriptions.Domain.Pricing;

namespace Billora.Subscriptions.Domain.Plans;

public sealed class Plan : Entity
{
    public Guid TenantId { get; }
    public PlanName Name { get; private set; }
    public PlanDescription? Description { get; private set; }
    public PlanTerms Terms { get; private set; }
    public bool IsActive { get; private set; }

    private Plan(
        Guid id,
        Guid tenantId,
        PlanName name,
        PlanDescription? description,
        PlanTerms terms,
        bool isActive) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Description = description;
        Terms = terms;
        IsActive = isActive;
    }

    public static Plan Create(
        Guid tenantId,
        PlanName name,
        PlanDescription? description,
        PlanTerms terms)
    {
        var plan = new Plan(NewId(), tenantId, name, description, terms, true);

        plan.RaiseDomainEvent(new PlanCreatedDomainEvent(plan.Id));

        return plan;
    }

    public void UpdateDetails(PlanName name, PlanDescription? description)
    {
        Name = name;
        Description = description;
    }

    public void ChangeTerms(PlanTerms terms) => Terms = terms;

    public Result Deactivate()
    {
        if (!IsActive)
        {
            return Result.Failure(PlanErrors.AlreadyInactive);
        }

        IsActive = false;

        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
        {
            return Result.Failure(PlanErrors.AlreadyActive);
        }

        IsActive = true;

        return Result.Success();
    }

}
