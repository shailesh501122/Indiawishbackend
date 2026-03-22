using BuildingBlocks.Domain.Common;

namespace Properties.Domain;

public sealed class PropertyAmenity : BaseEntity
{
    public Guid PropertyId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private PropertyAmenity() { }

    public PropertyAmenity(Guid propertyId, string name)
    {
        PropertyId = propertyId;
        Name = name;
    }
}
