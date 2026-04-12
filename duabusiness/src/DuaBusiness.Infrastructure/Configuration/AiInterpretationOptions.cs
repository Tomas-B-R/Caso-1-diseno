namespace DuaBusiness.Infrastructure.Configuration;

public sealed class AiInterpretationOptions
{
    public const string SectionName = "AiInterpretation";

    public string Endpoint { get; init; } = string.Empty;
    public string ApiVersion { get; init; } = string.Empty;
    public string ModelName { get; init; } = "gpt-4o-mini";
}
