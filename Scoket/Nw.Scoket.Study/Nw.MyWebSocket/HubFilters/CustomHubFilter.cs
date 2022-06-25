using Microsoft.AspNetCore.SignalR;
using Nw.MyWebSocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.MyWebSocket.HubFilters
{
    public class CustomHubFilter : Attribute, IHubFilter
    {
        public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
        {
            Console.WriteLine($"Calling hub method '{invocationContext.HubMethodName}'");
            try
            {
                if (invocationContext.HubMethodArguments.Count > 0)
                {
                    object[] arguments = invocationContext.HubMethodArguments.ToArray();
                    if (arguments[0] is ChatMsg chatMsg)
                    {

                        if (chatMsg.Message == null)
                        {
                            throw new HubException("请求参数不对，请重试");
                        }
                        chatMsg.Message = $"服务端检查过参数了:{chatMsg.Message}";

                        arguments[0] = chatMsg;
                        invocationContext = new HubInvocationContext(invocationContext.Context,
                            invocationContext.ServiceProvider,
                            invocationContext.Hub,
                            invocationContext.HubMethod,
                            arguments
                            );
                    }

                }
                return await next(invocationContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception calling '{invocationContext.HubMethodName}': {ex}");
                throw;
            }
        }

        // Optional method
        public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
        {
            return next(context);
        }

        // Optional method
        public Task OnDisconnectedAsync(
            HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
        {
            return next(context, exception);
        }
    }
}
