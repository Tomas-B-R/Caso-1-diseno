using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace DuaBusiness.Infrastructure.Processing;

public sealed class DuaDocumentGenerator : IDuaDocumentGenerator
{
    private readonly Configuration.BlobStorageOptions _blobStorageOptions;

    public DuaDocumentGenerator(IOptions<Configuration.BlobStorageOptions> blobStorageOptions)
    {
        _blobStorageOptions = blobStorageOptions.Value;
    }

    public Task<DownloadDuaDocumentResult> GenerateAsync(GenerateDuaDocumentCommand command, CancellationToken cancellationToken)
    {
        var result = new DownloadDuaDocumentResult(
            command.JobId,
            $"dua-{command.JobId:N}.docx",
            $"https://storage.placeholder.local/{_blobStorageOptions.GeneratedContainer}/dua-{command.JobId:N}.docx",
            DateTimeOffset.UtcNow.AddHours(1));

        return Task.FromResult(result);
    }
}
