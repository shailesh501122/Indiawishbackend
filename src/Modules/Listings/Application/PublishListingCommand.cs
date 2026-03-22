using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Interfaces;
using BuildingBlocks.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Listings.Domain;

namespace Listings.Application;

public sealed record PublishListingCommand(Guid PropertyId, ListingType ListingType, bool IsFeatured) : ICommand<Guid>;

public sealed class PublishListingCommandValidator : AbstractValidator<PublishListingCommand>
{
    public PublishListingCommandValidator() => RuleFor(x => x.PropertyId).NotEmpty();
}

public sealed class PublishListingCommandHandler(IApplicationDbContext dbContext) : MediatR.IRequestHandler<PublishListingCommand, Guid>
{
    public async Task<Guid> Handle(PublishListingCommand request, CancellationToken cancellationToken)
    {
        var property = await dbContext.Properties.FirstAsync(x => x.Id == request.PropertyId, cancellationToken);
        property.SetStatus(PropertyStatus.Published);
        var slug = $"{property.City}-{property.Area}-{property.Title}".ToLowerInvariant().Replace(' ', '-');
        var listing = new Listing(request.PropertyId, slug, request.ListingType, request.IsFeatured);
        listing.Approve();
        await dbContext.Listings.AddAsync(listing, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return listing.Id;
    }
}
