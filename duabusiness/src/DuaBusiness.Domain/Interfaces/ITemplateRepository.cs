using DuaBusiness.Domain.Entities;

namespace DuaBusiness.Domain.Interfaces;

public interface ITemplateRepository
{
    Task<DuaTemplate?> GetAsync(Guid templateId, CancellationToken cancellationToken);

    Task UpsertAsync(DuaTemplate template, CancellationToken cancellationToken);
}
