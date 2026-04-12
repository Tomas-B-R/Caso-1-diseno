using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Domain.ValueObjects;

namespace DuaBusiness.Application.Interfaces;

public interface IFileStorageService
{
    Task<IReadOnlyCollection<StorageReference>> ReserveUploadLocationsAsync(
        Guid jobId,
        IReadOnlyCollection<DocumentUploadDescriptor> documents,
        CancellationToken cancellationToken);
}
