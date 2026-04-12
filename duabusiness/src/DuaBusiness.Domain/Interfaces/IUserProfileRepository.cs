using DuaBusiness.Domain.Entities;

namespace DuaBusiness.Domain.Interfaces;

public interface IUserProfileRepository
{
    Task<UserProfile?> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken);
}
