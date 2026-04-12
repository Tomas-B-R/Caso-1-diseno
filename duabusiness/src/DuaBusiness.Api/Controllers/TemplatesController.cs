using DuaBusiness.Api.Contracts;
using DuaBusiness.Application.Contracts.Templates;
using DuaBusiness.Application.Interfaces;
using DuaBusiness.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuaBusiness.Api.Controllers;

[ApiController]
[Route("api/v1/templates")]
[Authorize(Policy = AuthorizationPolicyCatalog.ManagerPolicy)]
public sealed class TemplatesController : ControllerBase
{
    private readonly ITemplateApplicationService _templateApplicationService;

    public TemplatesController(ITemplateApplicationService templateApplicationService)
    {
        _templateApplicationService = templateApplicationService;
    }

    [HttpGet("{templateId:guid}")]
    [ProducesResponseType(typeof(TemplateDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<TemplateDto>> GetAsync(Guid templateId, CancellationToken cancellationToken)
    {
        var response = await _templateApplicationService.GetAsync(templateId, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{templateId:guid}")]
    [ProducesResponseType(typeof(TemplateDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<TemplateDto>> UpsertAsync(
        Guid templateId,
        [FromBody] UpdateDuaTemplateApiRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateDuaTemplateCommand(
            templateId,
            request.Name,
            request.Description,
            request.StoragePath,
            request.RequiredFields,
            request.PublishImmediately);

        var response = await _templateApplicationService.UpsertAsync(command, cancellationToken);
        return Ok(response);
    }
}
