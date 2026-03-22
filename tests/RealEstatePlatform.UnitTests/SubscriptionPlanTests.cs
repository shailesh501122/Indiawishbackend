using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.ValueObjects;
using FluentAssertions;
using Subscriptions.Domain;
using Xunit;

namespace RealEstatePlatform.UnitTests;

public sealed class SubscriptionPlanTests
{
    [Fact]
    public void Premium_Plan_Should_Allow_Unlimited_Listings()
    {
        var plan = new SubscriptionPlan("Premium", SubscriptionPlanType.Premium, Money.Inr(1999), -1, true, true, 30);
        var subscription = new UserSubscription(Guid.NewGuid(), plan.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(30));
        subscription.CanPublishMore(plan.ListingQuota).Should().BeTrue();
    }
}
