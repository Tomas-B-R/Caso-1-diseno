namespace DuaBusiness.Domain.Events;

public sealed record ProcessingJobSubmittedDomainEvent(Guid JobId, string CorrelationId, DateTimeOffset SubmittedAtUtc);
