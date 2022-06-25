using Nw.MyWebSocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.MyWebSocket.Jwt
{
    public interface IJwtService
    {
        string GetToken(UserDto user);
    }
}
