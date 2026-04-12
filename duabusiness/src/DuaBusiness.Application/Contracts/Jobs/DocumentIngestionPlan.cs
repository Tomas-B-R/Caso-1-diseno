namespace DuaBusiness.Application.Contracts.Jobs;

public sealed record DocumentIngestionPlan(
    Guid JobId,
    IReadOnlyCollection<string> SelectedStrategies,
    bool RequiresOcr,
    bool RequiresSemanticInterpretation);
