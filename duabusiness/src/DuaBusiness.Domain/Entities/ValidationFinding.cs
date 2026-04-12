using DuaBusiness.Domain.Enums;
using DuaBusiness.Domain.ValueObjects;

namespace DuaBusiness.Domain.Entities;

public sealed class ValidationFinding
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Code { get; init; } = string.Empty;
    public string FieldName { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public ValidationSeverity Severity { get; init; }
    public ConfidenceScore ConfidenceScore { get; init; } = ConfidenceScore.Empty;
    public bool RequiresManualReview => Severity == ValidationSeverity.Error || ConfidenceScore.RequiresManualReview;
}
