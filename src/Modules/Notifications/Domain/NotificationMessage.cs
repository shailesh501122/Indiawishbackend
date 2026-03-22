using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Enums;

namespace Notifications.Domain;

public sealed class NotificationMessage : AggregateRoot
{
    public Guid UserId { get; private set; }
    public NotificationChannel Channel { get; private set; }
    public string Subject { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public bool IsSent { get; private set; }

    private NotificationMessage() { }

    public NotificationMessage(Guid userId, NotificationChannel channel, string subject, string content)
    {
        UserId = userId;
        Channel = channel;
        Subject = subject;
        Content = content;
    }

    public void MarkSent()
    {
        IsSent = true;
        Touch();
    }
}
