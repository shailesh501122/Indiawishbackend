using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.API.Contracts;
using Subscriptions.Application;

namespace RealEstatePlatform.API.Controllers;

[ApiController]
[Route("api/subscriptions")]
public sealed class SubscriptionsController(ISender sender) : ControllerBase
{
    [Authorize]
    [HttpPost("activate")]
    public async Task<ActionResult<Guid>> Activate(ActivateSubscriptionRequest request, CancellationToken cancellationToken)
        => Ok(await sender.Send(new ActivateSubscriptionCommand(request.UserId, request.PlanId, request.PaymentProvider), cancellationToken));
}
