using BuildingBlocks.Domain.Enums;
namespace BuildingBlocks.Application.Interfaces;
public interface INotificationSender
{
    Task SendAsync(string recipient, string subject, string body, NotificationChannel channel, CancellationToken cancellationToken);
}
