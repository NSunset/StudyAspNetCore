using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.ResourceOwnerMode
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes = new[]
        {
            new ApiScope
            {
                Name="sample_api",
                Description="Sample API"
            }
        };

        public static IEnumerable<Client> Clients = new[]
        {
            ///指定客户端授权模式为资源拥有者凭证授权
            new Client
            {
                ClientId="sample_client",
                ClientSecrets =
                {
                    new Secret("sample_secret".Sha256())
                },
                AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                AllowedScopes =
                {
                    "sample_api"
                }
            }
        };

        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser
            {
                SubjectId="1",
                Username="admin",
                Password="123",
            }
        };
    }
}
