using BuildingBlocks.Application.Interfaces;
using BuildingBlocks.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RealEstatePlatform.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public sealed class AdminController(IApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet("analytics")]
    public async Task<ActionResult<object>> GetAnalytics(CancellationToken cancellationToken)
    {
        var totalUsers = await dbContext.Users.CountAsync(cancellationToken);
        var activeListings = await dbContext.Listings.CountAsync(listing => listing.IsApproved, cancellationToken);
        var revenue = await dbContext.Payments.Where(payment => payment.Status == PaymentStatus.Succeeded).SumAsync(payment => payment.Amount.Amount, cancellationToken);
        return Ok(new { totalUsers, activeListings, revenue, activeSubscriptions = await dbContext.UserSubscriptions.CountAsync(x => x.Status == SubscriptionStatus.Active, cancellationToken) });
    }
}
