using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.API.Contracts;
using Reviews.Application;

namespace RealEstatePlatform.API.Controllers;

[ApiController]
[Route("api/reviews")]
public sealed class ReviewsController(ISender sender) : ControllerBase
{
    [Authorize(Roles = "Tenant")]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateReviewRequest request, CancellationToken cancellationToken)
        => Ok(await sender.Send(new CreateReviewCommand(request.PropertyId, request.ReviewerId, request.OwnerId, request.Rating, request.Comment), cancellationToken));
}
