using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.Common.JWTHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.WebApi.JwtHelper
{
    public class JwtAuthentication
    {
        public static void Validate(IServiceCollection service, IConfiguration configuration)
        {
            //jwt配置文件配置
            JwtConfigure jwtConfigure = configuration.GetSection(JwtConfigure.Key).Get<JwtConfigure>();

            var logger = service.BuildServiceProvider().GetService<ILogger<JwtAuthentication>>();

            bool getKey = RsaHelper.TryGetKeyParameters(jwtConfigure.RsaPublicKeyPath, false, out RSA rSA);
            if (!getKey)
            {
                logger.LogError($"jwt获取公钥失败:{jwtConfigure.RsaPublicKeyPath}");
                throw new Exception("jwt获取公钥失败");
            }

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //是否验证签发者
                        ValidateIssuer = true,
                        //签发者
                        ValidIssuer = jwtConfigure.Issuer,

                        //是否验证订阅者
                        ValidateAudience = true,
                        //订阅者
                        ValidAudience = jwtConfigure.Audience,

                        //是否验证过期时间
                        ValidateLifetime = true,
                        //是否验证签发者key
                        ValidateIssuerSigningKey = true,
                        //Rsa加密方式
                        IssuerSigningKey = new RsaSecurityKey(rSA)

                        //自定义验证:有参数,key,token,validationParameters
                        //IssuerSigningKeyValidator= CustomValidator,
                        //有参数,Issuer,token,validationParameters
                        //IssuerValidator= 
                        //有参数,Audience,token,validationParameters
                        //AudienceValidator =

                    };

                    //如果验证不通过，可以给事件注册动作。这里是指定返回结果
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            //终止.netcore 默认返回数据
                            context.HandleResponse();

                            //构建返回数据
                            ApiResult result = ApiResult.Error("没有权限，请登录",
                                                StatusCodes.Status401Unauthorized);
                            //设置返回数据信息
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = StatusCodes.Status200OK;
                            //吧返回信息写入response
                            context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                            return Task.CompletedTask;
                        }
                    };
                });

        }

        /// <summary>
        /// 自定义验证
        /// </summary>
        /// <param name="securityKey"></param>
        /// <param name="securityToken"></param>
        /// <param name="validationParameters"></param>
        /// <returns></returns>
        private static bool CustomValidator(SecurityKey securityKey, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            return true;
        }

    }
}
