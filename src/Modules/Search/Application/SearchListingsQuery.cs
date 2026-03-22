using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Search.Application;

public sealed record SearchListingsQuery(string? SearchText, string? City, decimal? MinPrice, decimal? MaxPrice, int? Bhk, int Page = 1, int PageSize = 12) : IQuery<PagedResult<ListingSearchResult>>;
public sealed record PagedResult<T>(IReadOnlyCollection<T> Items, int TotalCount, int Page, int PageSize);

public sealed class SearchListingsQueryHandler(IApplicationDbContext dbContext) : MediatR.IRequestHandler<SearchListingsQuery, PagedResult<ListingSearchResult>>
{
    public async Task<PagedResult<ListingSearchResult>> Handle(SearchListingsQuery request, CancellationToken cancellationToken)
    {
        var query = from listing in dbContext.Listings.AsNoTracking()
                    join property in dbContext.Properties.AsNoTracking() on listing.PropertyId equals property.Id
                    where listing.IsApproved && property.Status == BuildingBlocks.Domain.Enums.PropertyStatus.Published
                    select new { listing, property };

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var searchText = request.SearchText.ToLowerInvariant();
            query = query.Where(x => x.property.Title.ToLower().Contains(searchText) || x.property.Description.ToLower().Contains(searchText));
        }
        if (!string.IsNullOrWhiteSpace(request.City)) query = query.Where(x => x.property.City == request.City);
        if (request.Bhk.HasValue) query = query.Where(x => x.property.Bhk == request.Bhk.Value);
        if (request.MinPrice.HasValue) query = query.Where(x => x.property.Price.Amount >= request.MinPrice.Value);
        if (request.MaxPrice.HasValue) query = query.Where(x => x.property.Price.Amount <= request.MaxPrice.Value);

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.OrderByDescending(x => x.listing.IsFeatured).ThenByDescending(x => x.listing.PublishedOnUtc)
            .Skip((request.Page - 1) * request.PageSize).Take(request.PageSize)
            .Select(x => new ListingSearchResult(x.listing.Id, x.listing.Slug, x.property.Title, x.property.City, x.property.Area, x.property.Price.Amount, x.listing.IsFeatured, x.property.Bhk, x.property.PropertyType.ToString()))
            .ToListAsync(cancellationToken);
        return new PagedResult<ListingSearchResult>(items, totalCount, request.Page, request.PageSize);
    }
}
