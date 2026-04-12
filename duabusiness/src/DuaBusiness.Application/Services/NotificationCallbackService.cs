using DuaBusiness.Application.Contracts.Notifications;
using DuaBusiness.Application.Interfaces;

namespace DuaBusiness.Application.Services;

public sealed class NotificationCallbackService : INotificationCallbackService
{
    private readonly IAuditTrailWriter _auditTrailWriter;

    public NotificationCallbackService(IAuditTrailWriter auditTrailWriter)
    {
        _auditTrailWriter = auditTrailWriter;
    }

    public async Task<NotificationCallbackReceipt> ReceiveAsync(
        Guid jobId,
        string providerMessageId,
        string status,
        CancellationToken cancellationToken)
    {
        await _auditTrailWriter.WriteAsync("notification.callback.received", jobId.ToString(), status, cancellationToken);

        return new NotificationCallbackReceipt(jobId, providerMessageId, status, DateTimeOffset.UtcNow);
    }
}
