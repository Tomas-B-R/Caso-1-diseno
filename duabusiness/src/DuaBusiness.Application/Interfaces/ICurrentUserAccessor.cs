namespace DuaBusiness.Application.Interfaces;

public interface ICurrentUserAccessor
{
    string ExternalUserId { get; }

    string DisplayName { get; }
}
