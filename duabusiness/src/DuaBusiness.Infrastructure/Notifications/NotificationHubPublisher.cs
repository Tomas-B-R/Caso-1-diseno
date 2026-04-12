using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DuaBusiness.Infrastructure.Notifications;

public sealed class NotificationHubPublisher : INotificationPublisher
{
    private readonly Configuration.NotificationHubOptions _options;
    private readonly ILogger<NotificationHubPublisher> _logger;

    public NotificationHubPublisher(
        IOptions<Configuration.NotificationHubOptions> options,
        ILogger<NotificationHubPublisher> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public Task PublishJobAcceptedAsync(CreateProcessingJobResult result, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Queued accepted notification for hub {HubName}. JobId: {JobId}, CorrelationId: {CorrelationId}",
            _options.HubName,
            result.JobId,
            result.CorrelationId);

        return Task.CompletedTask;
    }

    public Task PublishJobStatusAsync(ProcessingJobStatusDto status, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Queued status notification for hub {HubName}. JobId: {JobId}, Status: {Status}, Stage: {Stage}",
            _options.HubName,
            status.JobId,
            status.Status,
            status.Stage);

        return Task.CompletedTask;
    }
}
