namespace DuaBusiness.Infrastructure.Configuration;

public sealed class ObservabilityOptions
{
    public const string SectionName = "Observability";

    public string ApplicationInsightsConnectionString { get; init; } = string.Empty;
    public bool EnableTracing { get; init; } = true;
}
