using DuaBusiness.Domain.Interfaces;

namespace DuaBusiness.Infrastructure.Persistence;

public sealed class SqlUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public SqlUnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
