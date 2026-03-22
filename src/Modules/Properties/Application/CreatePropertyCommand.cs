using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Interfaces;
using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.ValueObjects;
using FluentValidation;
using Properties.Domain;

namespace Properties.Application;

public sealed record CreatePropertyCommand(Guid OwnerId, string Title, string Description, PropertyType PropertyType, decimal Price, string City, string Area, decimal Latitude, decimal Longitude, int Bhk, IReadOnlyCollection<string> Amenities, IReadOnlyCollection<string> MediaUrls) : ICommand<Guid>;

public sealed class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(180);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Bhk).GreaterThanOrEqualTo(0);
    }
}

public sealed class CreatePropertyCommandHandler(IApplicationDbContext dbContext) : MediatR.IRequestHandler<CreatePropertyCommand, Guid>
{
    public async Task<Guid> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = new Property(request.OwnerId, request.Title, request.Description, request.PropertyType, Money.Inr(request.Price), request.City, request.Area, GeoPoint.Create(request.Latitude, request.Longitude), request.Bhk);
        property.ReplaceAmenities(request.Amenities);
        foreach (var mediaUrl in request.MediaUrls)
            property.AddMedia(mediaUrl, mediaUrl.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) ? "video" : "image");
        await dbContext.Properties.AddAsync(property, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return property.Id;
    }
}
