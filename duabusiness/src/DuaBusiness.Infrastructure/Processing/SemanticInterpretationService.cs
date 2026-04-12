using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace DuaBusiness.Infrastructure.Processing;

public sealed class SemanticInterpretationService : ISemanticInterpretationService
{
    private readonly Configuration.AiInterpretationOptions _options;

    public SemanticInterpretationService(IOptions<Configuration.AiInterpretationOptions> options)
    {
        _options = options.Value;
    }

    public Task<SemanticInterpretationResult> InterpretAsync(Guid jobId, CancellationToken cancellationToken)
    {
        var result = new SemanticInterpretationResult(
            jobId,
            ["ImporterName", "ExporterName", "CustomsValue"],
            _options.ModelName,
            DateTimeOffset.UtcNow);

        return Task.FromResult(result);
    }
}
