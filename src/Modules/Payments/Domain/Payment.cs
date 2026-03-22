using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.ValueObjects;

namespace Payments.Domain;

public sealed class Payment : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid? SubscriptionId { get; private set; }
    public Money Amount { get; private set; } = Money.Inr(0);
    public string Provider { get; private set; } = string.Empty;
    public string ExternalReference { get; private set; } = string.Empty;
    public PaymentStatus Status { get; private set; } = PaymentStatus.Pending;

    private Payment() { }

    public Payment(Guid userId, Guid? subscriptionId, Money amount, string provider)
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
        Amount = amount;
        Provider = provider;
    }

    public void MarkSuccessful(string externalReference)
    {
        ExternalReference = externalReference;
        Status = PaymentStatus.Succeeded;
        Touch();
    }

    public void MarkFailed(string externalReference)
    {
        ExternalReference = externalReference;
        Status = PaymentStatus.Failed;
        Touch();
    }
}
