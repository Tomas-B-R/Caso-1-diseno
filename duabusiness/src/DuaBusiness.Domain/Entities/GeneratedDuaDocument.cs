using DuaBusiness.Domain.ValueObjects;

namespace DuaBusiness.Domain.Entities;

public sealed class GeneratedDuaDocument
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FileName { get; init; } = "dua-document.docx";
    public StorageReference StorageReference { get; init; } = new("generated", "dua-document.docx", "generated/dua-document.docx");
    public DateTimeOffset GeneratedAtUtc { get; init; } = DateTimeOffset.UtcNow;
    public TemplateVersion TemplateVersion { get; init; } = new(1, 0, 0);
    public string Checksum { get; init; } = string.Empty;
}
