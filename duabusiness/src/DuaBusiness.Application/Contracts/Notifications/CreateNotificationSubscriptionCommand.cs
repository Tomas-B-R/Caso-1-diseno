using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Application.Contracts.Notifications;

public sealed record CreateNotificationSubscriptionCommand(
    Guid JobId,
    string SubscriberId,
    NotificationChannel Channel,
    string Endpoint);
