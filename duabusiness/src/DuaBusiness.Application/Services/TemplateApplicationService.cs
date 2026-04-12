using DuaBusiness.Application.Contracts.Templates;
using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Entities;
using DuaBusiness.Domain.Interfaces;

namespace DuaBusiness.Application.Services;

public sealed class TemplateApplicationService : ITemplateApplicationService
{
    private readonly ITemplateRepository _templateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditTrailWriter _auditTrailWriter;

    public TemplateApplicationService(
        ITemplateRepository templateRepository,
        IUnitOfWork unitOfWork,
        IAuditTrailWriter auditTrailWriter)
    {
        _templateRepository = templateRepository;
        _unitOfWork = unitOfWork;
        _auditTrailWriter = auditTrailWriter;
    }

    public Task<TemplateDto> GetAsync(Guid templateId, CancellationToken cancellationToken)
    {
        var template = new TemplateDto(
            templateId,
            "Default DUA Template",
            "1.0.0",
            "Versioned template metadata scaffold.",
            true,
            ["ImporterName", "ExporterName", "CustomsValue"],
            DateTimeOffset.UtcNow);

        return Task.FromResult(template);
    }

    public async Task<TemplateDto> UpsertAsync(UpdateDuaTemplateCommand command, CancellationToken cancellationToken)
    {
        var template = new DuaTemplate
        {
            Id = command.TemplateId
        };

        template.UpdateMetadata(command.Name, command.Description, command.StoragePath, command.RequiredFields);

        if (command.PublishImmediately)
        {
            template.Activate();
        }

        await _templateRepository.UpsertAsync(template, cancellationToken);
        await _auditTrailWriter.WriteAsync("template.updated", command.TemplateId.ToString(), "Success", cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new TemplateDto(
            command.TemplateId,
            command.Name,
            "1.0.0",
            command.Description,
            template.IsActive,
            command.RequiredFields,
            DateTimeOffset.UtcNow);
    }
}
