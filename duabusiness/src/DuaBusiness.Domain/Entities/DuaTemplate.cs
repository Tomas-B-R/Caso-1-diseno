using DuaBusiness.Domain.ValueObjects;

namespace DuaBusiness.Domain.Entities;

public sealed class DuaTemplate
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public TemplateVersion Version { get; private set; } = new(1, 0, 0);
    public string Description { get; private set; } = string.Empty;
    public string StoragePath { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public IReadOnlyCollection<string> RequiredFields { get; private set; } = Array.Empty<string>();
    public DateTimeOffset LastUpdatedAtUtc { get; private set; } = DateTimeOffset.UtcNow;

    public void Activate() => IsActive = true;

    public void UpdateMetadata(string name, string description, string storagePath, IEnumerable<string> requiredFields)
    {
        Name = name;
        Description = description;
        StoragePath = storagePath;
        RequiredFields = requiredFields.ToArray();
        LastUpdatedAtUtc = DateTimeOffset.UtcNow;
    }
}
