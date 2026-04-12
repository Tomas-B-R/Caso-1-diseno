using DuaBusiness.Domain.Entities;
using DuaBusiness.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DuaBusiness.Infrastructure.Persistence.Repositories;

public sealed class NotificationSubscriptionRepository : INotificationSubscriptionRepository
{
    private readonly AppDbContext _dbContext;

    public NotificationSubscriptionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(NotificationSubscription subscription, CancellationToken cancellationToken)
    {
        await _dbContext.NotificationSubscriptions.AddAsync(subscription, cancellationToken);
    }

    public async Task<IReadOnlyCollection<NotificationSubscription>> ListByJobAsync(Guid jobId, CancellationToken cancellationToken)
    {
        return await _dbContext.NotificationSubscriptions
            .Where(item => item.JobId == jobId)
            .ToListAsync(cancellationToken);
    }
}
