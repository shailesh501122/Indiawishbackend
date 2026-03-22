using BuildingBlocks.Domain.Common;

namespace Properties.Domain;

public sealed class PropertyMedia : BaseEntity
{
    public Guid PropertyId { get; private set; }
    public string Url { get; private set; } = string.Empty;
    public string MediaType { get; private set; } = string.Empty;

    private PropertyMedia() { }

    public PropertyMedia(Guid propertyId, string url, string mediaType)
    {
        PropertyId = propertyId;
        Url = url;
        MediaType = mediaType;
    }
}
