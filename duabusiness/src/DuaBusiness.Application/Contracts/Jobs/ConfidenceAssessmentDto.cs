namespace DuaBusiness.Application.Contracts.Jobs;

public sealed record ConfidenceAssessmentDto(
    Guid JobId,
    decimal OverallConfidence,
    int AmbiguousFieldCount,
    bool RequiresManualReview);
