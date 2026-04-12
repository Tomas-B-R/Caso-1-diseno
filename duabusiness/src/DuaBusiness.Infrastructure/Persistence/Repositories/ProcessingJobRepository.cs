using DuaBusiness.Domain.Entities;
using DuaBusiness.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DuaBusiness.Infrastructure.Persistence.Repositories;

public sealed class ProcessingJobRepository : IProcessingJobRepository
{
    private readonly AppDbContext _dbContext;

    public ProcessingJobRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ProcessingJob?> GetAsync(Guid jobId, CancellationToken cancellationToken)
    {
        return _dbContext.ProcessingJobs.FirstOrDefaultAsync(item => item.Id == jobId, cancellationToken);
    }

    public async Task AddAsync(ProcessingJob job, CancellationToken cancellationToken)
    {
        await _dbContext.ProcessingJobs.AddAsync(job, cancellationToken);
    }

    public Task UpdateAsync(ProcessingJob job, CancellationToken cancellationToken)
    {
        _dbContext.ProcessingJobs.Update(job);
        return Task.CompletedTask;
    }
}
