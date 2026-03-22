using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Enums;

namespace Listings.Domain;

public sealed class Listing : AggregateRoot
{
    public Guid PropertyId { get; private set; }
    public string Slug { get; private set; } = string.Empty;
    public ListingType ListingType { get; private set; }
    public bool IsFeatured { get; private set; }
    public bool IsApproved { get; private set; }
    public DateTime PublishedOnUtc { get; private set; }
    public int ViewCount { get; private set; }

    private Listing() { }

    public Listing(Guid propertyId, string slug, ListingType listingType, bool isFeatured)
    {
        PropertyId = propertyId;
        Slug = slug;
        ListingType = listingType;
        IsFeatured = isFeatured;
        PublishedOnUtc = DateTime.UtcNow;
    }

    public void Approve()
    {
        IsApproved = true;
        Touch();
    }

    public void IncreaseViews() => ViewCount++;
}
