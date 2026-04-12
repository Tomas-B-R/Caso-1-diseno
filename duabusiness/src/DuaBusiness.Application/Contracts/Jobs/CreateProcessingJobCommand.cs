namespace DuaBusiness.Application.Contracts.Jobs;

public sealed record CreateProcessingJobCommand(
    Guid TemplateId,
    string ClientReference,
    string CorrelationId,
    string SubmittedBy,
    IReadOnlyCollection<DocumentUploadDescriptor> Documents);
