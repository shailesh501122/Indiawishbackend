using Microsoft.EntityFrameworkCore;
using Users.Domain;
using Properties.Domain;
using Listings.Domain;
using Bookings.Domain;
using Payments.Domain;
using Subscriptions.Domain;
using Reviews.Domain;
using Notifications.Domain;

namespace BuildingBlocks.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<AppUser> Users { get; }
    DbSet<UserRefreshToken> RefreshTokens { get; }
    DbSet<Property> Properties { get; }
    DbSet<Listing> Listings { get; }
    DbSet<Booking> Bookings { get; }
    DbSet<Payment> Payments { get; }
    DbSet<SubscriptionPlan> SubscriptionPlans { get; }
    DbSet<UserSubscription> UserSubscriptions { get; }
    DbSet<Review> Reviews { get; }
    DbSet<NotificationMessage> Notifications { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
