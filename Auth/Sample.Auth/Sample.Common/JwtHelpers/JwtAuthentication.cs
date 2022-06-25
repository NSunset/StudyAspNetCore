using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.JwtHelpers
{
    public static class JwtAuthentication
    {
        public static void AddHSJwtAuthentication(this IServiceCollection services, JwtConfigure jwtConfigure)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfigure.IssUser,
                        ValidAudience = jwtConfigure.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigure.Key)),
                        //AudienceValidator = (audiences, securityToken, validationParameters) =>
                        //{
                        //    return true;
                        //},
                        //LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                        //{
                        //    return false;
                        //}
                    };

                    options.Events = new JwtBearerEvents
                    {
                        //授权失败触发。如果自定义的授权没通过，这里会触发
                        OnForbidden = async context => {
                            await Task.Yield();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        }
                    };
                });
        }

        public static void AddRSJwtAuthentication(this IServiceCollection services, JwtConfigure jwtConfigure)
        {
            var dir = Directory.GetCurrentDirectory();
            if (!RSAHelper.TryGetKeyParameters(
                dir
                , false
                , out RSAParameters rSAParameters
                ))
            {
                throw new Exception($"指定文件夹下：{dir}没有公钥文件key.public.json");
            }
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfigure.IssUser,
                        ValidAudience = jwtConfigure.Audience,
                        IssuerSigningKey = new RsaSecurityKey(rSAParameters)
                    };
                    options.Events = new JwtBearerEvents
                    {
                        //授权失败触发。如果自定义的授权没通过，这里会触发
                        OnForbidden = async context =>
                        {
                            await Task.Yield();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        }
                    };
                });
        }
    }
}
