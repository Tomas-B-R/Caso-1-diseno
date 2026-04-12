using DuaBusiness.Application.Contracts.Jobs;

namespace DuaBusiness.Application.Interfaces;

public interface INotificationPublisher
{
    Task PublishJobAcceptedAsync(CreateProcessingJobResult result, CancellationToken cancellationToken);

    Task PublishJobStatusAsync(ProcessingJobStatusDto status, CancellationToken cancellationToken);
}
