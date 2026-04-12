namespace DuaBusiness.Domain.ValueObjects;

public sealed record StorageReference(string ContainerName, string BlobName, string RelativePath);
