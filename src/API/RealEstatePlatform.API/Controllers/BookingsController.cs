using Bookings.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.API.Contracts;

namespace RealEstatePlatform.API.Controllers;

[ApiController]
[Route("api/bookings")]
public sealed class BookingsController(ISender sender) : ControllerBase
{
    [Authorize(Roles = "Tenant")]
    [HttpPost("{propertyId:guid}")]
    public async Task<ActionResult<Guid>> Schedule(Guid propertyId, ScheduleVisitRequest request, CancellationToken cancellationToken)
        => Ok(await sender.Send(new ScheduleVisitCommand(propertyId, request.TenantId, request.VisitOnUtc, request.Notes), cancellationToken));
}
