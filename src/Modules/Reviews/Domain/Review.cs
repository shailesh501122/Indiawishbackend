using BuildingBlocks.Domain.Common;

namespace Reviews.Domain;

public sealed class Review : AggregateRoot
{
    public Guid PropertyId { get; private set; }
    public Guid ReviewerId { get; private set; }
    public Guid OwnerId { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; } = string.Empty;

    private Review() { }

    public Review(Guid propertyId, Guid reviewerId, Guid ownerId, int rating, string comment)
    {
        PropertyId = propertyId;
        ReviewerId = reviewerId;
        OwnerId = ownerId;
        Rating = rating;
        Comment = comment;
    }
}
