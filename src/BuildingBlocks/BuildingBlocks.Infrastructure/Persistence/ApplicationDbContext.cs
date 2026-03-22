using BuildingBlocks.Application.Interfaces;
using BuildingBlocks.Domain.Enums;
using Listings.Domain;
using Microsoft.EntityFrameworkCore;
using Notifications.Domain;
using Payments.Domain;
using Properties.Domain;
using Reviews.Domain;
using Subscriptions.Domain;
using Users.Domain;
using Bookings.Domain;

namespace BuildingBlocks.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<UserRefreshToken> RefreshTokens => Set<UserRefreshToken>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Listing> Listings => Set<Listing>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<NotificationMessage> Notifications => Set<NotificationMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<AppUser>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasMaxLength(180);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasMany<UserRole>("_roles").WithOne().HasForeignKey(x => x.UserId);
        });
        modelBuilder.Entity<UserRole>(builder => { builder.HasKey(x => x.Id); builder.Property(x => x.Role).HasConversion<string>().HasMaxLength(50); });
        modelBuilder.Entity<UserRefreshToken>(builder => { builder.HasKey(x => x.Id); builder.HasIndex(x => x.Token).IsUnique(); });

        modelBuilder.Entity<Property>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PropertyType).HasConversion<string>().HasMaxLength(50);
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(50);
            builder.OwnsOne(x => x.Price, money => { money.Property(x => x.Amount).HasColumnName("price_amount").HasPrecision(18,2); money.Property(x => x.Currency).HasColumnName("price_currency").HasMaxLength(3); });
            builder.OwnsOne(x => x.Coordinates, geo => { geo.Property(x => x.Latitude).HasColumnName("latitude").HasPrecision(10,7); geo.Property(x => x.Longitude).HasColumnName("longitude").HasPrecision(10,7); });
            builder.HasMany<PropertyAmenity>("_amenities").WithOne().HasForeignKey(x => x.PropertyId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany<PropertyMedia>("_media").WithOne().HasForeignKey(x => x.PropertyId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => new { x.City, x.Area, x.PropertyType, x.Status });
        });
        modelBuilder.Entity<PropertyAmenity>().HasKey(x => x.Id);
        modelBuilder.Entity<PropertyMedia>().HasKey(x => x.Id);

        modelBuilder.Entity<Listing>(builder => { builder.HasKey(x => x.Id); builder.Property(x => x.ListingType).HasConversion<string>().HasMaxLength(30); builder.HasIndex(x => x.Slug).IsUnique(); });
        modelBuilder.Entity<Booking>(builder => { builder.HasKey(x => x.Id); builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30); });
        modelBuilder.Entity<Payment>(builder => { builder.HasKey(x => x.Id); builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30); builder.OwnsOne(x => x.Amount, money => { money.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18,2); money.Property(x => x.Currency).HasColumnName("currency").HasMaxLength(3); }); });
        modelBuilder.Entity<SubscriptionPlan>(builder => { builder.HasKey(x => x.Id); builder.Property(x => x.PlanType).HasConversion<string>().HasMaxLength(30); builder.OwnsOne(x => x.Price, money => { money.Property(x => x.Amount).HasColumnName("price").HasPrecision(18,2); money.Property(x => x.Currency).HasColumnName("currency").HasMaxLength(3); }); });
        modelBuilder.Entity<UserSubscription>(builder => { builder.HasKey(x => x.Id); builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30); });
        modelBuilder.Entity<Review>().HasKey(x => x.Id);
        modelBuilder.Entity<NotificationMessage>(builder => { builder.HasKey(x => x.Id); builder.Property(x => x.Channel).HasConversion<string>().HasMaxLength(30); });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var ownerId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var propertyId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var listingId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var freePlanId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var premiumPlanId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var now = new DateTime(2026, 03, 22, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<AppUser>().HasData(
            new { Id = adminId, FullName = "System Admin", Email = "admin@indiawish.dev", PhoneNumber = "+919999999999", PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("Admin@123")), IsKycVerified = true, AvatarUrl = (string?)null, CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null },
            new { Id = ownerId, FullName = "Priya Sharma", Email = "owner@indiawish.dev", PhoneNumber = "+918888888888", PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("Owner@123")), IsKycVerified = true, AvatarUrl = (string?)null, CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null });
        modelBuilder.Entity<UserRole>().HasData(
            new { Id = Guid.Parse("77777777-7777-7777-7777-777777777771"), UserId = adminId, Role = RoleType.Admin, CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null },
            new { Id = Guid.Parse("77777777-7777-7777-7777-777777777772"), UserId = ownerId, Role = RoleType.Owner, CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null });
        modelBuilder.Entity<Property>().HasData(new { Id = propertyId, OwnerId = ownerId, Title = "Skyline Residency 2BHK", Description = "Premium semi-furnished apartment close to metro.", PropertyType = PropertyType.Flat, City = "Bengaluru", Area = "Whitefield", Bhk = 2, Status = PropertyStatus.Published, CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null, price_amount = 42000m, price_currency = "INR", latitude = 12.9698m, longitude = 77.7499m });
        modelBuilder.Entity<PropertyAmenity>().HasData(new { Id = Guid.Parse("88888888-8888-8888-8888-888888888881"), PropertyId = propertyId, Name = "Parking", CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null }, new { Id = Guid.Parse("88888888-8888-8888-8888-888888888882"), PropertyId = propertyId, Name = "Lift", CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null });
        modelBuilder.Entity<PropertyMedia>().HasData(new { Id = Guid.Parse("99999999-9999-9999-9999-999999999991"), PropertyId = propertyId, Url = "https://images.example.com/whitefield-2bhk.jpg", MediaType = "image", CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null });
        modelBuilder.Entity<Listing>().HasData(new { Id = listingId, PropertyId = propertyId, Slug = "bengaluru-whitefield-skyline-residency-2bhk", ListingType = ListingType.Rent, IsFeatured = true, IsApproved = true, PublishedOnUtc = now, ViewCount = 128, CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null });
        modelBuilder.Entity<SubscriptionPlan>().HasData(
            new { Id = freePlanId, Name = "Free", PlanType = SubscriptionPlanType.Free, ListingQuota = 2, AllowsFeaturedListings = false, PriorityVisibility = false, DurationInDays = 30, IsActive = true, CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null, price = 0m, currency = "INR" },
            new { Id = premiumPlanId, Name = "Premium", PlanType = SubscriptionPlanType.Premium, ListingQuota = -1, AllowsFeaturedListings = true, PriorityVisibility = true, DurationInDays = 30, IsActive = true, CreatedOnUtc = now, ModifiedOnUtc = (DateTime?)null, price = 1999m, currency = "INR" });
    }
}
