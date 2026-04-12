using DuaBusiness.Application.Contracts.Notifications;

namespace DuaBusiness.Application.Interfaces;

public interface INotificationSubscriptionService
{
    Task<NotificationSubscriptionDto> CreateAsync(
        CreateNotificationSubscriptionCommand command,
        CancellationToken cancellationToken);
}
