using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Entities;

namespace DuaBusiness.Infrastructure.Processing;

public sealed class ConfidenceFlagService : IConfidenceFlagService
{
    public Task<ConfidenceAssessmentDto> AssessAsync(
        Guid jobId,
        IReadOnlyCollection<ExtractedField> fields,
        CancellationToken cancellationToken)
    {
        var average = fields.Count == 0
            ? 0m
            : fields.Average(field => field.ConfidenceScore.Value);

        var ambiguousFields = fields.Count(field => field.RequiresManualReview);

        return Task.FromResult(new ConfidenceAssessmentDto(
            jobId,
            average,
            ambiguousFields,
            ambiguousFields > 0));
    }
}
