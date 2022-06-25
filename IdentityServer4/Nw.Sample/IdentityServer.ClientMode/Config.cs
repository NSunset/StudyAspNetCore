using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.ClientMode
{
    public static class Config
    {
        /// <summary>
        /// 受保护的资源Scopes范围
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes = new[]
        {
            new ApiScope
            {
                Name="sample_api",
                DisplayName="Sample API"
            }
        };

        /// <summary>
        /// 客户端
        /// </summary>
        public static IEnumerable<Client> Clients = new[]
        {
            new Client
            {
                ClientId="sample_client",
                ClientSecrets =
                {
                    new Secret("sample_client_secret".Sha256())
                },
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    "sample_api"
                }
            }
        };
    }
}
