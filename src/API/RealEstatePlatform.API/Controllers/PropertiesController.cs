using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Properties.Application;
using RealEstatePlatform.API.Contracts;

namespace RealEstatePlatform.API.Controllers;

[ApiController]
[Route("api/properties")]
public sealed class PropertiesController(ISender sender) : ControllerBase
{
    [Authorize(Roles = "Owner,Agent")]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromQuery] Guid ownerId, CreatePropertyRequest request, CancellationToken cancellationToken)
        => CreatedAtAction(nameof(Create), new { id = await sender.Send(new CreatePropertyCommand(ownerId, request.Title, request.Description, request.PropertyType, request.Price, request.City, request.Area, request.Latitude, request.Longitude, request.Bhk, request.Amenities, request.MediaUrls), cancellationToken) }, null);
}
