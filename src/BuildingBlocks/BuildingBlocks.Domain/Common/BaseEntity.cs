namespace BuildingBlocks.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedOnUtc { get; protected set; } = DateTime.UtcNow;
    public DateTime? ModifiedOnUtc { get; protected set; }

    protected void Touch() => ModifiedOnUtc = DateTime.UtcNow;
}
