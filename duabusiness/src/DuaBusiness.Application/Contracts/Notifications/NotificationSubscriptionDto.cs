using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Application.Contracts.Notifications;

public sealed record NotificationSubscriptionDto(
    Guid SubscriptionId,
    Guid JobId,
    NotificationChannel Channel,
    string Endpoint,
    bool IsActive,
    DateTimeOffset CreatedAtUtc);
