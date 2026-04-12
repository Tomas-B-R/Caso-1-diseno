using DuaBusiness.Api.Contracts;
using DuaBusiness.Application.Contracts.Notifications;
using DuaBusiness.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuaBusiness.Api.Controllers;

[ApiController]
[Route("api/v1/notification-callbacks")]
[AllowAnonymous]
public sealed class NotificationCallbacksController : ControllerBase
{
    private readonly INotificationCallbackService _notificationCallbackService;

    public NotificationCallbacksController(INotificationCallbackService notificationCallbackService)
    {
        _notificationCallbackService = notificationCallbackService;
    }

    [HttpPost("notification-hub")]
    [ProducesResponseType(typeof(NotificationCallbackReceipt), StatusCodes.Status200OK)]
    public async Task<ActionResult<NotificationCallbackReceipt>> ReceiveNotificationHubCallbackAsync(
        [FromBody] NotificationHubCallbackRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _notificationCallbackService.ReceiveAsync(
            request.JobId,
            request.ProviderMessageId,
            request.Status,
            cancellationToken);

        return Ok(response);
    }
}
