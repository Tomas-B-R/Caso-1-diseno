using DuaBusiness.Application.Contracts.Jobs;

namespace DuaBusiness.Application.Interfaces;

public interface IDuaDocumentGenerator
{
    Task<DownloadDuaDocumentResult> GenerateAsync(GenerateDuaDocumentCommand command, CancellationToken cancellationToken);
}
