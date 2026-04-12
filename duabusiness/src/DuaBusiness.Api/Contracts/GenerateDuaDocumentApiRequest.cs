namespace DuaBusiness.Api.Contracts;

public sealed class GenerateDuaDocumentApiRequest
{
    public Guid TemplateId { get; init; }

    public bool ForceManualReviewOverride { get; init; }
}
