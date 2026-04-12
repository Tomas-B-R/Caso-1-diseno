using Microsoft.AspNetCore.Http;

namespace DuaBusiness.Api.Contracts;

public sealed class CreateProcessingJobFormRequest
{
    public Guid TemplateId { get; init; }

    public string ClientReference { get; init; } = string.Empty;

    public List<IFormFile> Files { get; init; } = [];
}
