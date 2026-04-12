using DuaBusiness.Domain.ValueObjects;

namespace DuaBusiness.Domain.Entities;

public sealed class ExtractedField
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = string.Empty;
    public string? Value { get; init; }
    public Guid SourceDocumentId { get; init; }
    public ConfidenceScore ConfidenceScore { get; init; } = ConfidenceScore.Empty;
    public bool RequiresManualReview => ConfidenceScore.RequiresManualReview;
    public string ExtractionSource { get; init; } = string.Empty;
}
