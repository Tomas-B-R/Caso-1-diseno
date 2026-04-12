using DuaBusiness.Domain.Enums;
using DuaBusiness.Domain.ValueObjects;

namespace DuaBusiness.Domain.Entities;

public sealed class SourceDocument
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FileName { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
    public DocumentType DocumentType { get; init; }
    public long SizeInBytes { get; init; }
    public string Checksum { get; init; } = string.Empty;
    public DateTimeOffset UploadedAtUtc { get; init; } = DateTimeOffset.UtcNow;
    public StorageReference StorageReference { get; init; } = new("documents", "pending", "documents/pending");
}
