using DuaBusiness.Domain.Entities;

namespace DuaBusiness.Domain.Interfaces;

public interface INotificationSubscriptionRepository
{
    Task AddAsync(NotificationSubscription subscription, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<NotificationSubscription>> ListByJobAsync(Guid jobId, CancellationToken cancellationToken);
}
