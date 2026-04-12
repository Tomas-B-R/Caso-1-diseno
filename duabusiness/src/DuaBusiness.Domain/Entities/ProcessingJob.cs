using DuaBusiness.Domain.Enums;
using DuaBusiness.Domain.ValueObjects;

namespace DuaBusiness.Domain.Entities;

public sealed class ProcessingJob
{
    private readonly List<SourceDocument> _sourceDocuments = [];
    private readonly List<ExtractedField> _extractedFields = [];
    private readonly List<ValidationFinding> _validationFindings = [];

    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid TemplateId { get; private set; }
    public string ClientReference { get; private set; } = string.Empty;
    public string SubmittedBy { get; private set; } = string.Empty;
    public string CorrelationId { get; private set; } = string.Empty;
    public JobStatus Status { get; private set; } = JobStatus.Received;
    public ProcessingStage CurrentStage { get; private set; } = ProcessingStage.Intake;
    public ConfidenceScore OverallConfidence { get; private set; } = ConfidenceScore.Empty;
    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAtUtc { get; private set; }
    public GeneratedDuaDocument? GeneratedDocument { get; private set; }
    public IReadOnlyCollection<SourceDocument> SourceDocuments => _sourceDocuments;
    public IReadOnlyCollection<ExtractedField> ExtractedFields => _extractedFields;
    public IReadOnlyCollection<ValidationFinding> ValidationFindings => _validationFindings;

    public static ProcessingJob Create(
        Guid templateId,
        string clientReference,
        string submittedBy,
        string correlationId,
        IEnumerable<SourceDocument> sourceDocuments)
    {
        var job = new ProcessingJob
        {
            TemplateId = templateId,
            ClientReference = clientReference,
            SubmittedBy = submittedBy,
            CorrelationId = correlationId
        };

        job._sourceDocuments.AddRange(sourceDocuments);
        return job;
    }

    public void MarkQueued()
    {
        Status = JobStatus.Queued;
        CurrentStage = ProcessingStage.Storage;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void MarkInProgress(ProcessingStage stage)
    {
        Status = JobStatus.InProgress;
        CurrentStage = stage;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void ApplyExtraction(IEnumerable<ExtractedField> extractedFields, ConfidenceScore confidenceScore)
    {
        _extractedFields.Clear();
        _extractedFields.AddRange(extractedFields);
        OverallConfidence = confidenceScore;
        CurrentStage = ProcessingStage.Validation;
        Status = JobStatus.PendingValidation;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void ApplyValidation(IEnumerable<ValidationFinding> findings)
    {
        _validationFindings.Clear();
        _validationFindings.AddRange(findings);
        Status = findings.Any(item => item.RequiresManualReview)
            ? JobStatus.PendingManualReview
            : JobStatus.InProgress;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void MarkGenerated(GeneratedDuaDocument generatedDocument, ConfidenceScore confidenceScore)
    {
        GeneratedDocument = generatedDocument;
        OverallConfidence = confidenceScore;
        CurrentStage = ProcessingStage.DuaGeneration;
        Status = ValidationFindings.Any(item => item.Severity == ValidationSeverity.Warning)
            ? JobStatus.CompletedWithWarnings
            : JobStatus.Completed;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void MarkFailed()
    {
        Status = JobStatus.Failed;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
    }
}
