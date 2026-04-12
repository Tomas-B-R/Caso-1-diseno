using DuaBusiness.Api.Contracts;
using DuaBusiness.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuaBusiness.Api.Controllers;

[ApiController]
[Route("api/v1/identity")]
[Authorize]
public sealed class IdentityController : ControllerBase
{
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public IdentityController(ICurrentUserAccessor currentUserAccessor)
    {
        _currentUserAccessor = currentUserAccessor;
    }

    [HttpGet("me")]
    [ProducesResponseType(typeof(CurrentUserResponse), StatusCodes.Status200OK)]
    public ActionResult<CurrentUserResponse> GetCurrentAsync()
    {
        return Ok(new CurrentUserResponse(_currentUserAccessor.ExternalUserId, _currentUserAccessor.DisplayName));
    }
}
