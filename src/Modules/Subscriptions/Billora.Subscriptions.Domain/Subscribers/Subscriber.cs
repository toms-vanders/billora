using Billora.SharedKernel;
using Billora.Subscriptions.Domain.Subscribers.Events;

namespace Billora.Subscriptions.Domain.Subscribers;

public sealed class Subscriber : Entity
{
    public Guid TenantId { get; }
    public ExternalId ExternalId { get; }
    public SubscriberName Name { get; private set; }
    public Email Email { get; private set; }

    private Subscriber(Guid id, Guid tenantId, ExternalId externalId, SubscriberName name, Email email)
    : base(id)
    {
        TenantId = tenantId;
        ExternalId = externalId;
        Name = name;
        Email = email;
    }

    public static Subscriber Create(Guid tenantId, ExternalId externalId, SubscriberName name, Email email)
    {
        var subscriber = new Subscriber(NewId(), tenantId, externalId, name, email);

        subscriber.RaiseDomainEvent(new SubscriberCreatedDomainEvent(subscriber.Id));

        return subscriber;
    }

    public void Rename(SubscriberName name) => Name = name;

    public void ChangeEmail(Email email) => Email = email;
}
