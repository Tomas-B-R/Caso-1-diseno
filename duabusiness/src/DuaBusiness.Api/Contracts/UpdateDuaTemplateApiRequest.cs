namespace DuaBusiness.Api.Contracts;

public sealed class UpdateDuaTemplateApiRequest
{
    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string StoragePath { get; init; } = string.Empty;

    public List<string> RequiredFields { get; init; } = [];

    public bool PublishImmediately { get; init; }
}
