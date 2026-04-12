using DuaBusiness.Application.Contracts.Jobs;

namespace DuaBusiness.Application.Interfaces;

public interface ISemanticInterpretationService
{
    Task<SemanticInterpretationResult> InterpretAsync(Guid jobId, CancellationToken cancellationToken);
}
