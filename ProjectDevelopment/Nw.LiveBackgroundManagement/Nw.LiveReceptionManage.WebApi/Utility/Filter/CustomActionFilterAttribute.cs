using Microsoft.AspNetCore.Mvc.Filters;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.WebApi.Utility.Filter
{
    /// <summary>
    /// 在进入api方法前做操作
    /// </summary>
    public class CustomActionFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool isAuthorization =
                context.ActionDescriptor.EndpointMetadata.Any(a => a.GetType() == typeof(Microsoft.AspNetCore.Authorization.AuthorizeAttribute));

            bool isAllowAnonymous =
                context.ActionDescriptor.EndpointMetadata.Any(a => a.GetType() == typeof(Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute));
            if (isAuthorization && !isAllowAnonymous)
            {
                IEnumerable<Claim> claims = context.HttpContext.User.Claims;
                Claim claimId = claims.FirstOrDefault(c => c.Type == nameof(CSUser.Id));
                Claim claimName = claims.FirstOrDefault(c => c.Type == nameof(CSUser.Name));

                CurrentUser.Id = Convert.ToInt32(claimId.Value);
                CurrentUser.Name = claimName.Value;

            }

        }
    }
}
