using Nw.MyWebSocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.MyWebSocket.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(ResultMsg result);
    }
}
