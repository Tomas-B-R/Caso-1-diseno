using System.Security.Claims;
using DuaBusiness.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DuaBusiness.Infrastructure.Security;

public sealed class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string ExternalUserId =>
        _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "anonymous";

    public string DisplayName =>
        _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "DUA operator";
}
