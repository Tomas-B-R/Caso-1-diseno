namespace DuaBusiness.Infrastructure.Configuration;

public sealed class ApiSecurityOptions
{
    public const string SectionName = "Authentication";

    public string Authority { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public bool RequireHttpsMetadata { get; init; } = true;
    public int RateLimitPerMinute { get; init; } = 120;
}
