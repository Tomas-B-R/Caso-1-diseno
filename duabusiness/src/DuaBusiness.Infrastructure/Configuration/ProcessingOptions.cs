namespace DuaBusiness.Infrastructure.Configuration;

public sealed class ProcessingOptions
{
    public const string SectionName = "Processing";

    public int MaxUploadSizeMb { get; init; } = 50;
    public int MaxRetryAttempts { get; init; } = 3;
    public string[] AcceptedContentTypes { get; init; } = [];
}
