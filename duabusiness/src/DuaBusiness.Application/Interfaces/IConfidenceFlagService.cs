using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Domain.Entities;

namespace DuaBusiness.Application.Interfaces;

public interface IConfidenceFlagService
{
    Task<ConfidenceAssessmentDto> AssessAsync(
        Guid jobId,
        IReadOnlyCollection<ExtractedField> fields,
        CancellationToken cancellationToken);
}
