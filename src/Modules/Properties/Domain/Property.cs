using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.ValueObjects;

namespace Properties.Domain;

public sealed class Property : AggregateRoot
{
    private readonly List<PropertyAmenity> _amenities = new();
    private readonly List<PropertyMedia> _media = new();

    public Guid OwnerId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public PropertyType PropertyType { get; private set; }
    public Money Price { get; private set; } = Money.Inr(0);
    public string City { get; private set; } = string.Empty;
    public string Area { get; private set; } = string.Empty;
    public GeoPoint Coordinates { get; private set; } = GeoPoint.Create(0, 0);
    public int Bhk { get; private set; }
    public PropertyStatus Status { get; private set; } = PropertyStatus.Draft;
    public IReadOnlyCollection<PropertyAmenity> Amenities => _amenities;
    public IReadOnlyCollection<PropertyMedia> Media => _media;

    private Property() { }

    public Property(Guid ownerId, string title, string description, PropertyType propertyType, Money price, string city, string area, GeoPoint coordinates, int bhk)
    {
        OwnerId = ownerId;
        Title = title;
        Description = description;
        PropertyType = propertyType;
        Price = price;
        City = city;
        Area = area;
        Coordinates = coordinates;
        Bhk = bhk;
    }

    public void SetStatus(PropertyStatus status)
    {
        Status = status;
        Touch();
    }

    public void ReplaceAmenities(IEnumerable<string> amenities)
    {
        _amenities.Clear();
        _amenities.AddRange(amenities.Select(value => new PropertyAmenity(Id, value)));
        Touch();
    }

    public void AddMedia(string url, string mediaType)
    {
        _media.Add(new PropertyMedia(Id, url, mediaType));
        Touch();
    }
}
