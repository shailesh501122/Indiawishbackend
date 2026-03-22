using Listings.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.API.Contracts;
using Search.Application;

namespace RealEstatePlatform.API.Controllers;

[ApiController]
[Route("api/listings")]
public sealed class ListingsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<ListingSearchResult>>> Search([FromQuery] SearchListingsQuery query, CancellationToken cancellationToken)
        => Ok(await sender.Send(query, cancellationToken));

    [Authorize(Roles = "Owner,Agent")]
    [HttpPost("{propertyId:guid}/publish")]
    public async Task<ActionResult<Guid>> Publish(Guid propertyId, PublishListingRequest request, CancellationToken cancellationToken)
        => Ok(await sender.Send(new PublishListingCommand(propertyId, request.ListingType, request.IsFeatured), cancellationToken));
}
