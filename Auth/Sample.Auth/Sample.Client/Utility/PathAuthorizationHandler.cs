using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sample.Client.Utility
{
    public class PathAuthorizationHandler : AuthorizationHandler<PathRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PathRequirement requirement)
        {
            HttpContext httpContext = context.Resource as DefaultHttpContext;
            if (context.User != null && context.User.Claims.Any(x => x.Type == ClaimTypes.Role))
            {
                var role = context.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role);

                if (role != null && role.Value == "admin" && httpContext.Request.Path.Equals("/api/product/get", StringComparison.OrdinalIgnoreCase))
                {
                    await Task.Yield();
                    context.Succeed(requirement);
                    return;
                }
            }
            context.Fail();
        }
    }
}
