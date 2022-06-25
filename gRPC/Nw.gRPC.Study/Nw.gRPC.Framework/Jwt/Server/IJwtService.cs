using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.gRPC.Framework
{
    public interface IJwtService
    {
        string GetToken(UserDto userDto);
    }
}
