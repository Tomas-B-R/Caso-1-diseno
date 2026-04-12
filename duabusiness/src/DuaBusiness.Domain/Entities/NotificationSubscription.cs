using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Domain.Entities;

public sealed class NotificationSubscription
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid JobId { get; init; }
    public string SubscriberId { get; init; } = string.Empty;
    public NotificationChannel Channel { get; init; }
    public string Endpoint { get; init; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;

    public void Disable() => IsActive = false;
}
