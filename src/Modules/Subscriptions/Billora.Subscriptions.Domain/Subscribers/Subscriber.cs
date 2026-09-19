using Billora.SharedKernel;
using Billora.Subscriptions.Domain.Subscribers.Events;

namespace Billora.Subscriptions.Domain.Subscribers;

public sealed class Subscriber : Entity
{
    private Subscriber(Guid id, Guid tenantId, ExternalId externalId, SubscriberName name, Email email)
        : base(id)
    {
        TenantId = tenantId;
        ExternalId = externalId;
        Name = name;
        Email = email;
    }

    public Guid TenantId { get; private set; }
    public ExternalId ExternalId { get; private set; }
    public SubscriberName Name { get; private set; }
    public Email Email { get; private set; }

    public static Subscriber Create(Guid tenantId, ExternalId externalId, SubscriberName name, Email email)
    {
        var subscriber = new Subscriber(NewId(), tenantId, externalId, name, email);

        subscriber.RaiseDomainEvent(new SubscriberCreatedDomainEvent(subscriber.Id));

        return subscriber;
    }
}
