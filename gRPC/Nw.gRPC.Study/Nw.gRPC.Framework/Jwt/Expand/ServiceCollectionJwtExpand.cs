using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nw.gRPC.Framework
{
    public static class ServiceCollectionJwtExpand
    {
        public static void JwtValidate(this IServiceCollection services)
        {
            JwtConfigure jwtConfigure = services.BuildServiceProvider().GetService<JwtConfigure>();
            //使用RSA公钥解密
            {
                //bool getKey1 = RSAHelper.TryGetKeyParameters(jwtConfigure.RsaPrivateKeyPath, true, out RSA rSA1);
                bool getKey = RSAHelper.TryGetKeyParameters(jwtConfigure.RsaPublicKeyPath, false, out RSA rSA);
                if (!getKey)
                    throw new Exception("RSA公钥路径生成RSA失败");
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfigure.Issuer,
                        ValidAudience = jwtConfigure.Audience,
                        IssuerSigningKey = new RsaSecurityKey(rSA),//指定RSA公钥解密
                        IssuerSigningKeyValidator = CustomValidator
                    };
                });
            }

            //使用HS对称可逆方式解密
            {
                //SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigure.SecurityKey));
                //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                //.AddJwtBearer(options => {
                //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                //    {
                //        ValidateIssuer = true,
                //        ValidateAudience = true,
                //        ValidateLifetime = true,
                //        ValidateIssuerSigningKey = true,
                //        ValidIssuer = jwtConfigure.Issuer,
                //        ValidAudience = jwtConfigure.Audience,
                //        IssuerSigningKey = securityKey,
                //        IssuerSigningKeyValidator = CustomValidator
                //    };
                //});
            }


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
