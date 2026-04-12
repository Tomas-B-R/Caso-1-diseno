using DuaBusiness.Api.Contracts;
using DuaBusiness.Application.Contracts.Notifications;
using DuaBusiness.Application.Interfaces;
using DuaBusiness.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuaBusiness.Api.Controllers;

[ApiController]
[Route("api/v1/notification-subscriptions")]
[Authorize]
public sealed class NotificationsController : ControllerBase
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly INotificationSubscriptionService _notificationSubscriptionService;

    public NotificationsController(
        ICurrentUserAccessor currentUserAccessor,
        INotificationSubscriptionService notificationSubscriptionService)
    {
        _currentUserAccessor = currentUserAccessor;
        _notificationSubscriptionService = notificationSubscriptionService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(NotificationSubscriptionDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<NotificationSubscriptionDto>> CreateAsync(
        [FromBody] CreateNotificationSubscriptionApiRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateNotificationSubscriptionCommand(
            request.JobId,
            _currentUserAccessor.ExternalUserId,
            request.Channel,
            request.Endpoint);

        var response = await _notificationSubscriptionService.CreateAsync(command, cancellationToken);
        return CreatedAtAction(nameof(CreateAsync), new { subscriptionId = response.SubscriptionId }, response);
    }
}
