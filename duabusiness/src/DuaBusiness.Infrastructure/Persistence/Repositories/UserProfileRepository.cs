using DuaBusiness.Domain.Entities;
using DuaBusiness.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DuaBusiness.Infrastructure.Persistence.Repositories;

public sealed class UserProfileRepository : IUserProfileRepository
{
    private readonly AppDbContext _dbContext;

    public UserProfileRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<UserProfile?> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken)
    {
        return _dbContext.UserProfiles.FirstOrDefaultAsync(item => item.ExternalId == externalId, cancellationToken);
    }
}
