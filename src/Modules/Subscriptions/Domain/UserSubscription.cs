using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Enums;

namespace Subscriptions.Domain;

public sealed class UserSubscription : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid PlanId { get; private set; }
    public SubscriptionStatus Status { get; private set; } = SubscriptionStatus.Pending;
    public DateTime StartsOnUtc { get; private set; }
    public DateTime EndsOnUtc { get; private set; }
    public int ListingsConsumed { get; private set; }

    private UserSubscription() { }

    public UserSubscription(Guid userId, Guid planId, DateTime startsOnUtc, DateTime endsOnUtc)
    {
        UserId = userId;
        PlanId = planId;
        StartsOnUtc = startsOnUtc;
        EndsOnUtc = endsOnUtc;
        Status = SubscriptionStatus.Active;
    }

    public bool CanPublishMore(int listingQuota) => listingQuota < 0 || ListingsConsumed < listingQuota;
    public void ConsumeListing() { ListingsConsumed++; Touch(); }
    public void Expire() { Status = SubscriptionStatus.Expired; Touch(); }
    public void Renew(DateTime newEndsOnUtc) { EndsOnUtc = newEndsOnUtc; Status = SubscriptionStatus.Active; Touch(); }
}
