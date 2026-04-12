using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Infrastructure.Processing;

public sealed class DocumentIngestionOrchestrator : IDocumentIngestionOrchestrator
{
    private readonly IReadOnlyCollection<IExtractionStrategy> _strategies;

    public DocumentIngestionOrchestrator(IEnumerable<IExtractionStrategy> strategies)
    {
        _strategies = strategies.ToArray();
    }

    public Task<DocumentIngestionPlan> PlanAsync(
        Guid jobId,
        IReadOnlyCollection<DocumentUploadDescriptor> documents,
        CancellationToken cancellationToken)
    {
        var selectedStrategies = documents
            .Select(document => _strategies.FirstOrDefault(strategy => strategy.CanHandle(document.DocumentType))?.Name ?? "FallbackReview")
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var requiresOcr = documents.Any(document => document.DocumentType == DocumentType.Image || document.DocumentType == DocumentType.Pdf);
        var requiresSemanticInterpretation = true;

        return Task.FromResult(new DocumentIngestionPlan(jobId, selectedStrategies, requiresOcr, requiresSemanticInterpretation));
    }
}
