using BuildingBlocks.Domain.Enums;

namespace RealEstatePlatform.API.Contracts;

public sealed record RegisterRequest(string FullName, string Email, string PhoneNumber, string Password, IReadOnlyCollection<RoleType> Roles);
public sealed record CreatePropertyRequest(string Title, string Description, PropertyType PropertyType, decimal Price, string City, string Area, decimal Latitude, decimal Longitude, int Bhk, IReadOnlyCollection<string> Amenities, IReadOnlyCollection<string> MediaUrls);
public sealed record PublishListingRequest(ListingType ListingType, bool IsFeatured);
public sealed record ScheduleVisitRequest(Guid TenantId, DateTime VisitOnUtc, string Notes);
public sealed record ActivateSubscriptionRequest(Guid UserId, Guid PlanId, string PaymentProvider);
public sealed record CreateReviewRequest(Guid PropertyId, Guid ReviewerId, Guid OwnerId, int Rating, string Comment);
