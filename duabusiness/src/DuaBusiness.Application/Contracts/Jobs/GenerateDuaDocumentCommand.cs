namespace DuaBusiness.Application.Contracts.Jobs;

public sealed record GenerateDuaDocumentCommand(
    Guid JobId,
    Guid TemplateId,
    bool ForceManualReviewOverride,
    string RequestedBy);
