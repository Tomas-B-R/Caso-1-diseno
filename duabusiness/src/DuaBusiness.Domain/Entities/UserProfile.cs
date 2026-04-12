using DuaBusiness.Domain.Enums;

namespace DuaBusiness.Domain.Entities;

public sealed class UserProfile
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string ExternalId { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string EmailAddress { get; init; } = string.Empty;
    public UserRole Role { get; init; }
    public IReadOnlyCollection<Permission> Permissions { get; init; } = Array.Empty<Permission>();
}
