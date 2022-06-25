using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common.JWTHelper
{
    public class JwtConfigure
    {
        public const string Key = "Jwt";
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string RsaPrivateKeyPath { get; set; }

        public string RsaPublicKeyPath { get; set; }

        public string SecurityKey { get; set; }

        public int AccessTokenExpires { get; set; }
        public int RefreshTokenExpires { get; set; }
    }
}
