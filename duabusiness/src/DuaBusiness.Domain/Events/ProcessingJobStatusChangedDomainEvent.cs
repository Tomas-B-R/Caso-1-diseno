using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Domain.Events;

public sealed record ProcessingJobStatusChangedDomainEvent(Guid JobId, JobStatus Status, ProcessingStage Stage, DateTimeOffset OccurredAtUtc);
