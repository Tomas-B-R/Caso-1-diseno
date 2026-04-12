using DuaBusiness.Application.Interfaces;

namespace DuaBusiness.Worker.HostedServices;

public sealed class ProcessingJobBackgroundService : BackgroundService
{
    private readonly ILogger<ProcessingJobBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ProcessingJobBackgroundService(
        ILogger<ProcessingJobBackgroundService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var semanticInterpreter = scope.ServiceProvider.GetRequiredService<ISemanticInterpretationService>();

            _logger.LogInformation("DUA processing worker heartbeat at {Timestamp}", DateTimeOffset.UtcNow);
            await semanticInterpreter.InterpretAsync(Guid.NewGuid(), stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
    }
}
