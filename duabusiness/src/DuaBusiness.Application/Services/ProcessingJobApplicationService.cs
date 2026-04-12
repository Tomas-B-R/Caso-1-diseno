using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Enums;
using DuaBusiness.Domain.Interfaces;

namespace DuaBusiness.Application.Services;

public sealed class ProcessingJobApplicationService : IProcessingJobApplicationService
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IProcessingJobRepository _processingJobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentIngestionOrchestrator _documentIngestionOrchestrator;
    private readonly IDuaDocumentGenerator _duaDocumentGenerator;
    private readonly INotificationPublisher _notificationPublisher;
    private readonly IAuditTrailWriter _auditTrailWriter;

    public ProcessingJobApplicationService(
        ICurrentUserAccessor currentUserAccessor,
        IProcessingJobRepository processingJobRepository,
        IUnitOfWork unitOfWork,
        IDocumentIngestionOrchestrator documentIngestionOrchestrator,
        IDuaDocumentGenerator duaDocumentGenerator,
        INotificationPublisher notificationPublisher,
        IAuditTrailWriter auditTrailWriter)
    {
        _currentUserAccessor = currentUserAccessor;
        _processingJobRepository = processingJobRepository;
        _unitOfWork = unitOfWork;
        _documentIngestionOrchestrator = documentIngestionOrchestrator;
        _duaDocumentGenerator = duaDocumentGenerator;
        _notificationPublisher = notificationPublisher;
        _auditTrailWriter = auditTrailWriter;
    }

    public async Task<CreateProcessingJobResult> CreateAsync(CreateProcessingJobCommand command, CancellationToken cancellationToken)
    {
        var jobId = Guid.NewGuid();

        await _documentIngestionOrchestrator.PlanAsync(jobId, command.Documents, cancellationToken);
        await _auditTrailWriter.WriteAsync("processing-job.accepted", jobId.ToString(), "Accepted", cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new CreateProcessingJobResult(
            jobId,
            JobStatus.Queued,
            command.CorrelationId,
            DateTimeOffset.UtcNow,
            $"Processing job accepted for {_currentUserAccessor.DisplayName}.");

        await _notificationPublisher.PublishJobAcceptedAsync(result, cancellationToken);
        return result;
    }

    public async Task<ProcessingJobStatusDto> GetAsync(Guid jobId, CancellationToken cancellationToken)
    {
        await _processingJobRepository.GetAsync(jobId, cancellationToken);

        return new ProcessingJobStatusDto(
            jobId,
            JobStatus.InProgress,
            ProcessingStage.SemanticInterpretation,
            SourceDocumentCount: 0,
            ValidationIssueCount: 0,
            RequiresManualReview: false,
            CorrelationId: $"corr-{jobId:N}",
            LastUpdatedAtUtc: DateTimeOffset.UtcNow);
    }

    public async Task<ProcessingJobStatusDto> GenerateAsync(GenerateDuaDocumentCommand command, CancellationToken cancellationToken)
    {
        await _duaDocumentGenerator.GenerateAsync(command, cancellationToken);
        await _auditTrailWriter.WriteAsync("processing-job.generate-requested", command.JobId.ToString(), "Accepted", cancellationToken);

        var result = new ProcessingJobStatusDto(
            command.JobId,
            JobStatus.InProgress,
            ProcessingStage.DuaGeneration,
            SourceDocumentCount: 0,
            ValidationIssueCount: 0,
            RequiresManualReview: false,
            CorrelationId: $"corr-{command.JobId:N}",
            LastUpdatedAtUtc: DateTimeOffset.UtcNow);

        await _notificationPublisher.PublishJobStatusAsync(result, cancellationToken);
        return result;
    }

    public Task<DownloadDuaDocumentResult> DownloadAsync(Guid jobId, CancellationToken cancellationToken)
    {
        var result = new DownloadDuaDocumentResult(
            jobId,
            $"dua-{jobId:N}.docx",
            $"https://storage.placeholder.local/generated/dua-{jobId:N}.docx",
            DateTimeOffset.UtcNow.AddMinutes(30));

        return Task.FromResult(result);
    }
}
