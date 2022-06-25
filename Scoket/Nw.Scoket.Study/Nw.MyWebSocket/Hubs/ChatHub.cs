using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nw.MyWebSocket.Models;

namespace Nw.MyWebSocket.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMsg msg)
        {

            ResultMsg resultMsg = new ResultMsg
            {
                IsOk = true,
                Body = msg
            };

            await Clients.All.SendAsync("ReceiveMessage", resultMsg);
        }
    }
}
