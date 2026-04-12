using DuaBusiness.Domain.Entities;
using DuaBusiness.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DuaBusiness.Infrastructure.Persistence.Repositories;

public sealed class TemplateRepository : ITemplateRepository
{
    private readonly AppDbContext _dbContext;

    public TemplateRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<DuaTemplate?> GetAsync(Guid templateId, CancellationToken cancellationToken)
    {
        return _dbContext.Templates.FirstOrDefaultAsync(item => item.Id == templateId, cancellationToken);
    }

    public async Task UpsertAsync(DuaTemplate template, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.Templates.AnyAsync(item => item.Id == template.Id, cancellationToken);

        if (exists)
        {
            _dbContext.Templates.Update(template);
            return;
        }

        await _dbContext.Templates.AddAsync(template, cancellationToken);
    }
}
