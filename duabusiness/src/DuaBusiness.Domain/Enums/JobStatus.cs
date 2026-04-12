namespace DuaBusiness.Domain.Enums;

public enum JobStatus
{
    Received = 0,
    Queued = 1,
    InProgress = 2,
    PendingValidation = 3,
    PendingManualReview = 4,
    Completed = 5,
    CompletedWithWarnings = 6,
    Failed = 7,
    Cancelled = 8
}
