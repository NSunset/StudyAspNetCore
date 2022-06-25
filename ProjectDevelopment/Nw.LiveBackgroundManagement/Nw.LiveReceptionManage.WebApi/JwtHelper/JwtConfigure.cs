using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.WebApi.JwtHelper
{
    public class JwtConfigure
    {
        public const string JwtAppsettings = "Jwt";
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string RsaPublicKeyPath { get; set; }

    }
}
