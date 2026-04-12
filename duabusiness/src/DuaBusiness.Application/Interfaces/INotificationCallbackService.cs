using DuaBusiness.Application.Contracts.Notifications;

namespace DuaBusiness.Application.Interfaces;

public interface INotificationCallbackService
{
    Task<NotificationCallbackReceipt> ReceiveAsync(
        Guid jobId,
        string providerMessageId,
        string status,
        CancellationToken cancellationToken);
}
