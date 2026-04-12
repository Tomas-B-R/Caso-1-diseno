namespace DuaBusiness.Infrastructure.Configuration;

public sealed class RetentionOptions
{
    public const string SectionName = "Retention";

    public int SourceFileDays { get; init; } = 30;
    public int GeneratedDuaDays { get; init; } = 180;
    public int AuditLogDays { get; init; } = 365;
}
