using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DuaBusiness.Infrastructure.Health;

public sealed class SqlDatabaseHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("SQL database binding registered."));
    }
}
