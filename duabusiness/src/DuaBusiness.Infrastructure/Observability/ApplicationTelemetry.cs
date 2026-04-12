using DuaBusiness.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace DuaBusiness.Infrastructure.Observability;

public sealed class ApplicationTelemetry : IAuditTrailWriter
{
    private readonly ILogger<ApplicationTelemetry> _logger;

    public ApplicationTelemetry(ILogger<ApplicationTelemetry> logger)
    {
        _logger = logger;
    }

    public Task WriteAsync(string eventName, string subjectId, string outcome, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Audit event {EventName} for {SubjectId} finished with outcome {Outcome}",
            eventName,
            subjectId,
            outcome);

        return Task.CompletedTask;
    }
}
