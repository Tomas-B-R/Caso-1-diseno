namespace DuaBusiness.Application.Contracts.Notifications;

public sealed record NotificationCallbackReceipt(
    Guid JobId,
    string ProviderMessageId,
    string Status,
    DateTimeOffset ReceivedAtUtc);
