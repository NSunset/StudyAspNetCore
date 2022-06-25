using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer.CodeMode.Persistence.Test
{
    public class Tests
    {
        private DbContextOptions<ConfigurationDbContext> options;
        private ConfigurationStoreOptions storeOptions;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ConfigurationDbContext>()
                   .UseMySql(
                   "Server=192.168.157.128;Database=IdentityServer;Uid=root;Pwd=root;charset=utf8",
                   new MySqlServerVersion(new Version(5, 0, 17))
                   ).Options;

            storeOptions = new ConfigurationStoreOptions();
        }

        [Test]
        public async Task Test1()
        {
            using (var configurationDb = new ConfigurationDbContext(options, storeOptions))
            {
                var clients = await configurationDb.Set<Client>().AsNoTracking().ToListAsync();
                Assert.Pass();
            }
        }

        [Test]
        public async Task AddClient_Test()
        {
            using (var configurationDb = new ConfigurationDbContext(options, storeOptions))
            {

                try
                {
                    configurationDb.Set<Client>().Add(new Client
                    {
                        ClientId = "sample_api",
                        ClientName = "mvc客户端",
                        ClientSecrets = new List<ClientSecret>
                        {
                            new ClientSecret
                            {
                                Created=DateTime.Now,
                                Description="mvc客户端秘钥",
                                Expiration=DateTime.Now.AddMonths(6),
                                Value="sample_client_secret",
                                Type="Sha256"
                            }
                        },
                        AllowedGrantTypes = new List<ClientGrantType>
                        {
                            new ClientGrantType()
                            {
                                GrantType="code"
                            }
                        },
                        RedirectUris = new List<ClientRedirectUri>
                        {
                            new ClientRedirectUri
                            {
                                RedirectUri="https://localhost:4001/signin-oidc"
                            }
                        },
                        PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri>
                        {
                            new ClientPostLogoutRedirectUri
                            {
                                PostLogoutRedirectUri= "https://localhost:4001/signout-callback-oidc"
                            }
                        },
                        RequireConsent = true,
                        AllowedScopes = new List<ClientScope>
                        {
                            new ClientScope
                            {
                                Scope="sample_api"
                            },
                            new ClientScope
                            {
                                Scope="openid"
                            },
                            new ClientScope
                            {
                                Scope="profile"
                            }
                        }
                    });

                    configurationDb.Set<IdentityResource>().Add(new IdentityResource
                    {
                        Name= "IdentityResource",
                        Created = DateTime.Now,
                        Properties = new List<IdentityResourceProperty>
                        {
                            new IdentityResourceProperty
                            {
                                Key="openid",
                                Value="openid"
                            },
                            new IdentityResourceProperty
                            {
                                Key="profile",
                                Value="profile"
                            }
                        }
                    });

                    configurationDb.Set<ApiScope>().Add(new ApiScope
                    {
                        Name= "ApiScope",
                        Properties = new List<ApiScopeProperty>
                        {
                            new ApiScopeProperty
                            {
                                Key="sample_api",
                                Value="sample_api"
                            }
                        }
                    });

                    configurationDb.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }


        [Test]
        public async Task Seed()
        {
            using (var context = new ConfigurationDbContext(options, storeOptions))
            {
                //if (!context.Clients.Any())
                //{
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    await context.SaveChangesAsync();
                //}

                //if (!context.IdentityResources.Any())
                //{
                    foreach (var resource in Config.IdentityResource)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    await context.SaveChangesAsync();
                //}

                //if (!context.ApiScopes.Any())
                //{
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    await context.SaveChangesAsync();
                //}
            }

            Assert.Pass();
        }
    }
}