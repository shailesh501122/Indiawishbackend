using Bookings.Domain;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Interfaces;
using BuildingBlocks.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notifications.Domain;

namespace Bookings.Application;

public sealed record ScheduleVisitCommand(Guid PropertyId, Guid TenantId, DateTime VisitOnUtc, string Notes) : ICommand<Guid>;

public sealed class ScheduleVisitCommandValidator : AbstractValidator<ScheduleVisitCommand>
{
    public ScheduleVisitCommandValidator()
    {
        RuleFor(x => x.PropertyId).NotEmpty();
        RuleFor(x => x.TenantId).NotEmpty();
        RuleFor(x => x.VisitOnUtc).GreaterThan(DateTime.UtcNow);
    }
}

public sealed class ScheduleVisitCommandHandler(IApplicationDbContext dbContext) : MediatR.IRequestHandler<ScheduleVisitCommand, Guid>
{
    public async Task<Guid> Handle(ScheduleVisitCommand request, CancellationToken cancellationToken)
    {
        var property = await dbContext.Properties.AsNoTracking().FirstAsync(x => x.Id == request.PropertyId, cancellationToken);
        var booking = new Booking(request.PropertyId, request.TenantId, property.OwnerId, request.VisitOnUtc, request.Notes);
        await dbContext.Bookings.AddAsync(booking, cancellationToken);
        await dbContext.Notifications.AddAsync(new NotificationMessage(property.OwnerId, NotificationChannel.Email, "New property visit scheduled", $"Visit requested for {property.Title}"), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return booking.Id;
    }
}
