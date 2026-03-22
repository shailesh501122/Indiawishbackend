using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Enums;

namespace Bookings.Domain;

public sealed class Booking : AggregateRoot
{
    public Guid PropertyId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid OwnerId { get; private set; }
    public DateTime VisitOnUtc { get; private set; }
    public BookingStatus Status { get; private set; } = BookingStatus.Pending;
    public string Notes { get; private set; } = string.Empty;

    private Booking() { }

    public Booking(Guid propertyId, Guid tenantId, Guid ownerId, DateTime visitOnUtc, string notes)
    {
        PropertyId = propertyId;
        TenantId = tenantId;
        OwnerId = ownerId;
        VisitOnUtc = visitOnUtc;
        Notes = notes;
    }

    public void Approve() => Status = BookingStatus.Approved;
    public void Reject() => Status = BookingStatus.Rejected;
}
