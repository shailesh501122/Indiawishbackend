using BuildingBlocks.Application.Interfaces;
using BuildingBlocks.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Infrastructure.Services;

public sealed class CompositeNotificationSender(ILogger<CompositeNotificationSender> logger) : INotificationSender
{
    public Task SendAsync(string recipient, string subject, string body, NotificationChannel channel, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending {Channel} to {Recipient}: {Subject}", channel, recipient, subject);
        return Task.CompletedTask;
    }
}
