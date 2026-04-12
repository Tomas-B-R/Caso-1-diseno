namespace DuaBusiness.Application.Contracts.Templates;

public sealed record UpdateDuaTemplateCommand(
    Guid TemplateId,
    string Name,
    string Description,
    string StoragePath,
    IReadOnlyCollection<string> RequiredFields,
    bool PublishImmediately);
