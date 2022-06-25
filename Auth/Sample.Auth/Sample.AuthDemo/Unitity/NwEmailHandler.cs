using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.AuthDemo.Unitity
{
    public class NwEmailHandler : AuthorizationHandler<EmailRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailRequirement requirement)
        {
            if (context.User != null)
            {
                var email = context.User.Claims.SingleOrDefault(u => u.Type == System.Security.Claims.ClaimTypes.Email)?.Value;

                if (email != null && email.EndsWith("@nw.com", StringComparison.OrdinalIgnoreCase))
                {
                    await Task.Yield();
                    context.Succeed(requirement);
                }
                else
                {
                    //当前写的策略是两种邮箱，只要有一个能通过就行了，所以没有写失败，否则只会验证一个失败了就不验证另一个了
                    //context.Fail();
                }
            }
        }
    }
}
