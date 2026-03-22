using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.ValueObjects;

namespace Subscriptions.Domain;

public sealed class SubscriptionPlan : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public SubscriptionPlanType PlanType { get; private set; }
    public Money Price { get; private set; } = Money.Inr(0);
    public int ListingQuota { get; private set; }
    public bool AllowsFeaturedListings { get; private set; }
    public bool PriorityVisibility { get; private set; }
    public int DurationInDays { get; private set; }
    public bool IsActive { get; private set; }

    private SubscriptionPlan() { }

    public SubscriptionPlan(string name, SubscriptionPlanType planType, Money price, int listingQuota, bool allowsFeaturedListings, bool priorityVisibility, int durationInDays)
    {
        Name = name;
        PlanType = planType;
        Price = price;
        ListingQuota = listingQuota;
        AllowsFeaturedListings = allowsFeaturedListings;
        PriorityVisibility = priorityVisibility;
        DurationInDays = durationInDays;
        IsActive = true;
    }
}
