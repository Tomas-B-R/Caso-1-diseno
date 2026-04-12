using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace DuaBusiness.Infrastructure.Storage;

public sealed class BlobFileStorageService : IFileStorageService
{
    private readonly Configuration.BlobStorageOptions _options;

    public BlobFileStorageService(IOptions<Configuration.BlobStorageOptions> options)
    {
        _options = options.Value;
    }

    public Task<IReadOnlyCollection<StorageReference>> ReserveUploadLocationsAsync(
        Guid jobId,
        IReadOnlyCollection<DocumentUploadDescriptor> documents,
        CancellationToken cancellationToken)
    {
        var locations = documents
            .Select(document => new StorageReference(
                _options.DocumentsContainer,
                $"{jobId:N}/{document.FileName}",
                $"{_options.DocumentsContainer}/{jobId:N}/{document.FileName}"))
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<StorageReference>>(locations);
    }
}
