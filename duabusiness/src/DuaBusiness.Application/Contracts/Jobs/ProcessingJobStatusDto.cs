using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Application.Contracts.Jobs;

public sealed record ProcessingJobStatusDto(
    Guid JobId,
    JobStatus Status,
    ProcessingStage Stage,
    int SourceDocumentCount,
    int ValidationIssueCount,
    bool RequiresManualReview,
    string CorrelationId,
    DateTimeOffset LastUpdatedAtUtc);
