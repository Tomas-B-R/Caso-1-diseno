using DuaBusiness.Api.Contracts;
using DuaBusiness.Application.Contracts.Jobs;
using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Enums;
using DuaBusiness.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuaBusiness.Api.Controllers;

[ApiController]
[Route("api/v1/processing-jobs")]
[Authorize(Policy = AuthorizationPolicyCatalog.CustomsAgentPolicy)]
public sealed class ProcessingJobsController : ControllerBase
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IProcessingJobApplicationService _processingJobApplicationService;

    public ProcessingJobsController(
        ICurrentUserAccessor currentUserAccessor,
        IProcessingJobApplicationService processingJobApplicationService)
    {
        _currentUserAccessor = currentUserAccessor;
        _processingJobApplicationService = processingJobApplicationService;
    }

    [HttpPost]
    [RequestSizeLimit(50 * 1024 * 1024)]
    [ProducesResponseType(typeof(CreateProcessingJobResult), StatusCodes.Status202Accepted)]
    public async Task<ActionResult<CreateProcessingJobResult>> CreateAsync(
        [FromForm] CreateProcessingJobFormRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProcessingJobCommand(
            request.TemplateId,
            request.ClientReference,
            HttpContext.TraceIdentifier,
            _currentUserAccessor.DisplayName,
            request.Files.Select(file => new DocumentUploadDescriptor(
                file.FileName,
                file.ContentType,
                file.Length,
                DetermineDocumentType(file.ContentType, file.FileName)))
            .ToArray());

        var response = await _processingJobApplicationService.CreateAsync(command, cancellationToken);
        return AcceptedAtAction(nameof(GetByIdAsync), new { jobId = response.JobId }, response);
    }

    [HttpGet("{jobId:guid}")]
    [ProducesResponseType(typeof(ProcessingJobStatusDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProcessingJobStatusDto>> GetByIdAsync(Guid jobId, CancellationToken cancellationToken)
    {
        var response = await _processingJobApplicationService.GetAsync(jobId, cancellationToken);
        return Ok(response);
    }

    [HttpPost("{jobId:guid}/generate")]
    [ProducesResponseType(typeof(ProcessingJobStatusDto), StatusCodes.Status202Accepted)]
    public async Task<ActionResult<ProcessingJobStatusDto>> GenerateAsync(
        Guid jobId,
        [FromBody] GenerateDuaDocumentApiRequest request,
        CancellationToken cancellationToken)
    {
        var command = new GenerateDuaDocumentCommand(
            jobId,
            request.TemplateId,
            request.ForceManualReviewOverride,
            _currentUserAccessor.DisplayName);

        var response = await _processingJobApplicationService.GenerateAsync(command, cancellationToken);
        return Accepted(response);
    }

    [HttpGet("{jobId:guid}/download")]
    [ProducesResponseType(typeof(DownloadDuaDocumentResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<DownloadDuaDocumentResult>> DownloadAsync(Guid jobId, CancellationToken cancellationToken)
    {
        var response = await _processingJobApplicationService.DownloadAsync(jobId, cancellationToken);
        return Ok(response);
    }

    private static DocumentType DetermineDocumentType(string contentType, string fileName)
    {
        if (contentType.Contains("spreadsheet", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            return DocumentType.Excel;
        }

        if (contentType.Contains("word", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
        {
            return DocumentType.Word;
        }

        if (contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
        {
            return DocumentType.Image;
        }

        if (contentType.Contains("pdf", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            return DocumentType.Pdf;
        }

        return DocumentType.Unknown;
    }
}
