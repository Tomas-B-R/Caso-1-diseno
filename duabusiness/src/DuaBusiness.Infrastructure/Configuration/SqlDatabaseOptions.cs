namespace DuaBusiness.Infrastructure.Configuration;

public sealed class SqlDatabaseOptions
{
    public const string SectionName = "SqlDatabase";

    public string ConnectionString { get; init; } = string.Empty;
}
