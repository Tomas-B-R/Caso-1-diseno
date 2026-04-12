using DuaBusiness.Application.Contracts.Templates;

namespace DuaBusiness.Application.Interfaces;

public interface ITemplateApplicationService
{
    Task<TemplateDto> GetAsync(Guid templateId, CancellationToken cancellationToken);

    Task<TemplateDto> UpsertAsync(UpdateDuaTemplateCommand command, CancellationToken cancellationToken);
}
