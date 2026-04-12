namespace DuaBusiness.Application.Contracts.Templates;

public sealed record TemplateDto(
    Guid TemplateId,
    string Name,
    string Version,
    string Description,
    bool IsActive,
    IReadOnlyCollection<string> RequiredFields,
    DateTimeOffset LastUpdatedAtUtc);
