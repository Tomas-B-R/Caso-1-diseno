using DuaBusiness.Application.Contracts.Jobs;

namespace DuaBusiness.Application.Interfaces;

public interface IProcessingJobApplicationService
{
    Task<CreateProcessingJobResult> CreateAsync(CreateProcessingJobCommand command, CancellationToken cancellationToken);

    Task<ProcessingJobStatusDto> GetAsync(Guid jobId, CancellationToken cancellationToken);

    Task<ProcessingJobStatusDto> GenerateAsync(GenerateDuaDocumentCommand command, CancellationToken cancellationToken);

    Task<DownloadDuaDocumentResult> DownloadAsync(Guid jobId, CancellationToken cancellationToken);
}
