using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Nw.MyWebSocket.HubFilters;
using Nw.MyWebSocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.MyWebSocket.Hubs
{
    /// <summary>
    /// 使用鉴权
    /// </summary>
    [Authorize]
    public class NewChantHub : Hub<IChatClient>
    {
        //[HubMethodName("")]//更改服务端方法名
        [CustomHubFilter]
        public async Task SendMessage(ChatMsg chatMsg)
        {
            ResultMsg resultMsg = new ResultMsg
            {
                IsOk = true,
                Body = chatMsg
            };
            await Clients.All.ReceiveMessage(resultMsg);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"客户端{Context.ConnectionId}加入链接");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"客户端{Context.ConnectionId}断开链接");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
