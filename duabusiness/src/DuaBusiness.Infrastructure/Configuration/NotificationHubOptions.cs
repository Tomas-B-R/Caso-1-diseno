namespace DuaBusiness.Infrastructure.Configuration;

public sealed class NotificationHubOptions
{
    public const string SectionName = "NotificationHub";

    public string ConnectionString { get; init; } = string.Empty;
    public string HubName { get; init; } = "dua-status";
}
