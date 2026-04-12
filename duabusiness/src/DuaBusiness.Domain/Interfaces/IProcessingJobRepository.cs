using DuaBusiness.Domain.Entities;

namespace DuaBusiness.Domain.Interfaces;

public interface IProcessingJobRepository
{
    Task<ProcessingJob?> GetAsync(Guid jobId, CancellationToken cancellationToken);

    Task AddAsync(ProcessingJob job, CancellationToken cancellationToken);

    Task UpdateAsync(ProcessingJob job, CancellationToken cancellationToken);
}
