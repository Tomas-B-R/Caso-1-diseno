using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Api.Contracts;

public sealed class CreateNotificationSubscriptionApiRequest
{
    public Guid JobId { get; init; }

    public NotificationChannel Channel { get; init; }

    public string Endpoint { get; init; } = string.Empty;
}
