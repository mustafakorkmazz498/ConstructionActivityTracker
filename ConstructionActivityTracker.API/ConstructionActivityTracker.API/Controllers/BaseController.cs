using Application.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.API.Controllers;

public class BaseController : ControllerBase
{
    protected IMediator Mediator =>
        _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _mediator;

    protected string getIpAddress()
    {
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
        return ipAddress;
    }

    protected int getUserIdFromRequest()
    {
        var userIdClaim = HttpContext.User.GetIdClaim();
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new InvalidOperationException("User ID claim is missing or empty.");
        }

        if (int.TryParse(userIdClaim, out int userId))
        {
            return userId;
        }
        else
        {
            throw new InvalidOperationException("User ID claim is not a valid integer.");
        }
    }

}
