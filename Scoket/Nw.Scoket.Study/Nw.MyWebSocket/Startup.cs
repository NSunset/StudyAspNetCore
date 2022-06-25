using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nw.MyWebSocket.HubFilters;
using Nw.MyWebSocket.Hubs;
using Nw.MyWebSocket.Jwt;
using Nw.MyWebSocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Nw.MyWebSocket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nw.MyWebSocket", Version = "v1" });
            });

            services.AddSignalR(options =>
            {
                //注册SignR过滤器
                options.AddFilter<CustomHubFilter>();
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    ;

                });
            });

            services.AddSingleton<CustomHubFilter>();

            #region JWT鉴权

            JwtConfigure jwtConfigure = Configuration.GetSection("Jwt").Get<JwtConfigure>();
            services.AddSingleton<JwtConfigure>(jwtConfigure);
            services.AddTransient<IJwtService, RsaJwtService>();


            services.AddAuthentication(options =>
            {
                // Identity made Cookie authentication the default.
                // However, we want JWT Bearer Auth to be the default.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                bool getRsa = RSAHelper.TryGetKeyParameters(jwtConfigure.RsaPublicKeyPath, false, out RSA rSA);
                if (!getRsa)
                    throw new Exception("JWT鉴权获取RSA公钥失败，请查看");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfigure.Issuer,
                    ValidAudience = jwtConfigure.Audience,
                    IssuerSigningKey = new RsaSecurityKey(rSA),
                    IssuerSigningKeyValidator = (SecurityKey securityKey, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                    {
                        //也可以自己额外添加自定义鉴权逻辑
                        return true;
                    }
                };
                // Configure the Authority to the expected value for your authentication provider
                // This ensures the token is appropriately validated
                //options.Authority = /* TODO: Insert Authority URL here */;

                // We have to hook the OnMessageReceived event in order to
                // allow the JWT authentication handler to read the access
                // token from the query string when a WebSocket or 
                // Server-Sent Events request comes in.

                // Sending the access token in the query string is required due to
                // a limitation in Browser APIs. We restrict it to only calls to the
                // SignalR hub in this code.
                // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
                // for more information about security considerations when using
                // the query string to transmit the access token.
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var path = context.HttpContext.Request.Path;
                        if (path.StartsWithSegments("/NewChantHub"))
                        {
                            string accessToken = null;
                            if (context.Request.Query.ContainsKey("access_token"))
                            {
                                accessToken= context.Request.Query["access_token"];
                            }
                            else if(context.Request.Headers.ContainsKey("Authorization"))
                            {
                                accessToken = context.Request.Headers["Authorization"];
                            }
                            if (accessToken!=null)
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nw.MyWebSocket v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            //启用身份验证
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<ChatHub>("/chatHub");

                endpoints.MapHub<NewChantHub>("/NewChantHub");
            });
        }
    }
}
