using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;

namespace Nw.LiveBackgroundManagement.WebSite.Utility.Filters
{
    public class CustomLogActionFilterAttribute : Attribute, IActionFilter
    {

        private readonly ILogger<CustomLogActionFilterAttribute> _Ilogger = null;
        private readonly ICSUserservice _ICSUserservice = null;

        public CustomLogActionFilterAttribute(ILogger<CustomLogActionFilterAttribute> logger, ICSUserservice iCSUserservice)
        {
            this._Ilogger = logger;
            this._ICSUserservice = iCSUserservice;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            _Ilogger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(context.HttpContext.Request.Query));
            _ICSUserservice.Insert<SysLog>(new SysLog()
            {
                //UserName = context.HttpContext.User.Identity.Name
                CreatorId = 1,
                Detail = Newtonsoft.Json.JsonConvert.SerializeObject(context.HttpContext.Request.Query),
                LogType = 1//也可以定义枚举 
            }); ;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
