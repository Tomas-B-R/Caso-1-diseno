using DuaBusiness.Application.Contracts.Jobs;

namespace DuaBusiness.Application.Interfaces;

public interface IDocumentIngestionOrchestrator
{
    Task<DocumentIngestionPlan> PlanAsync(
        Guid jobId,
        IReadOnlyCollection<DocumentUploadDescriptor> documents,
        CancellationToken cancellationToken);
}
