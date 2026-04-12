namespace DuaBusiness.Api.Contracts;

public sealed class NotificationHubCallbackRequest
{
    public Guid JobId { get; init; }

    public string ProviderMessageId { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;
}
