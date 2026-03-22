namespace Search.Application;

public sealed record ListingSearchResult(Guid ListingId, string Slug, string Title, string City, string Area, decimal Price, bool IsFeatured, int Bhk, string PropertyType);
