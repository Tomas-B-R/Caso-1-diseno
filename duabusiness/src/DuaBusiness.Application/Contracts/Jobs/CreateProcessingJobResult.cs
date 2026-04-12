using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Application.Contracts.Jobs;

public sealed record CreateProcessingJobResult(
    Guid JobId,
    JobStatus Status,
    string CorrelationId,
    DateTimeOffset AcceptedAtUtc,
    string Message);
