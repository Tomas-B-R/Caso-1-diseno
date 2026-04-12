namespace DuaBusiness.Application.Contracts.Jobs;

public sealed record SemanticInterpretationResult(
    Guid JobId,
    IReadOnlyCollection<string> InterpretedFields,
    string ModelName,
    DateTimeOffset CompletedAtUtc);
