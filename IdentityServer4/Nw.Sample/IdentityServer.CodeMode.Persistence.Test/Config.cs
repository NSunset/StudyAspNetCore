using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.CodeMode.Persistence.Test
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes = new[]
        {
            new ApiScope
            {
                Name="sample_api",
                DisplayName="Sample API",
            }
        };

        public static IEnumerable<IdentityResource> IdentityResource = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<Client> Clients = new[]
        {
            new Client
            {
                ClientId="sample_client",
                ClientSecrets =
                {
                    new Secret("sample_client_secret".Sha256())
                },
                AllowedGrantTypes=GrantTypes.Code,
                RedirectUris={
                    //登录重定向
                    "https://localhost:4001/signin-oidc"
                },
                PostLogoutRedirectUris =
                {
                    //登出重定向
                    "https://localhost:4001/signout-callback-oidc"
                },
                AllowedScopes =
                {
                    "sample_api",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                //登录后需要用户授权
                RequireConsent=true
            }
        };

        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser
            {
                SubjectId="1",
                Username="admin",
                Password="123"
            }
        };


    }
}
