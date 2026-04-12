using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DuaBusiness.Api.Controllers;

[ApiController]
[Route("api/v1/health")]
[AllowAnonymous]
public sealed class HealthController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public HealthController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet("live")]
    public ActionResult Live() => Ok(new { status = "Live" });

    [HttpGet("ready")]
    public async Task<ActionResult> ReadyAsync(CancellationToken cancellationToken)
    {
        var report = await _healthCheckService.CheckHealthAsync(_ => true, cancellationToken);
        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                entry.Value.Description
            })
        };

        return report.Status == HealthStatus.Healthy ? Ok(response) : StatusCode(StatusCodes.Status503ServiceUnavailable, response);
    }
}
