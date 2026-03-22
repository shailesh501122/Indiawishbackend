using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Interfaces;
using FluentValidation;
using Reviews.Domain;

namespace Reviews.Application;

public sealed record CreateReviewCommand(Guid PropertyId, Guid ReviewerId, Guid OwnerId, int Rating, string Comment) : ICommand<Guid>;

public sealed class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.Comment).MaximumLength(500);
    }
}

public sealed class CreateReviewCommandHandler(IApplicationDbContext dbContext) : MediatR.IRequestHandler<CreateReviewCommand, Guid>
{
    public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review(request.PropertyId, request.ReviewerId, request.OwnerId, request.Rating, request.Comment);
        await dbContext.Reviews.AddAsync(review, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return review.Id;
    }
}
