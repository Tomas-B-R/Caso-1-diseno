using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DuaBusiness.Infrastructure.Health;

public sealed class NotificationHubHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("Notification hub binding registered."));
    }
}
