using Application.CrossCuttingConcerns.Exceptions;
using Application.Pipelines.Authorization;
using Application.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pipelines.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TRequest request2 = request;
        if ((_httpContextAccessor.HttpContext.User.GetRoleClaims() ?? throw new AuthorizationException("You are not authenticated.")).FirstOrDefault((string userRoleClaim) => userRoleClaim == "Admin" || request2.Roles.Contains(userRoleClaim)).IsNullOrEmpty())
        {
            throw new AuthorizationException("You are not authorized.");
        }

        return await next();
    }
}
