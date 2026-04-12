namespace DuaBusiness.Application.Interfaces;

public interface IAuditTrailWriter
{
    Task WriteAsync(string eventName, string subjectId, string outcome, CancellationToken cancellationToken);
}
