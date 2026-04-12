namespace DuaBusiness.Application.Contracts.Jobs;

public sealed record DownloadDuaDocumentResult(
    Guid JobId,
    string FileName,
    string DownloadUrl,
    DateTimeOffset ExpiresAtUtc);
