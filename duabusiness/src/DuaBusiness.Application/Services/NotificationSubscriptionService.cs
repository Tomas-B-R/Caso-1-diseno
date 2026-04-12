using DuaBusiness.Application.Contracts.Notifications;
using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Entities;
using DuaBusiness.Domain.Interfaces;

namespace DuaBusiness.Application.Services;

public sealed class NotificationSubscriptionService : INotificationSubscriptionService
{
    private readonly INotificationSubscriptionRepository _notificationSubscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationSubscriptionService(
        INotificationSubscriptionRepository notificationSubscriptionRepository,
        IUnitOfWork unitOfWork)
    {
        _notificationSubscriptionRepository = notificationSubscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<NotificationSubscriptionDto> CreateAsync(
        CreateNotificationSubscriptionCommand command,
        CancellationToken cancellationToken)
    {
        var subscription = new NotificationSubscription
        {
            JobId = command.JobId,
            SubscriberId = command.SubscriberId,
            Channel = command.Channel,
            Endpoint = command.Endpoint
        };

        await _notificationSubscriptionRepository.AddAsync(subscription, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new NotificationSubscriptionDto(
            subscription.Id,
            subscription.JobId,
            subscription.Channel,
            subscription.Endpoint,
            subscription.IsActive,
            subscription.CreatedAtUtc);
    }
}
