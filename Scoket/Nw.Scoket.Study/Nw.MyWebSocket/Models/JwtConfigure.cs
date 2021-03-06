using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.MyWebSocket.Models
{
    public class JwtConfigure
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string RsaPrivateKeyPath { get; set; }

        public string RsaPublicKeyPath { get; set; }

        public string SecurityKey { get; set; }

        public int Expires { get; set; }
    }
}
