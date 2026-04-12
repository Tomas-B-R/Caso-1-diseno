namespace DuaBusiness.Infrastructure.Configuration;

public sealed class BlobStorageOptions
{
    public const string SectionName = "Storage";

    public string ConnectionString { get; init; } = string.Empty;
    public string DocumentsContainer { get; init; } = "dua-documents";
    public string GeneratedContainer { get; init; } = "dua-generated";
}
