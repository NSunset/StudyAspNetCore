using Microsoft.Extensions.Options;
using Sample.Common.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;

namespace Sample.Common.Services
{
    public class DefaultRsJwtService : IJwtService
    {
        private readonly JwtConfigure options;

        public DefaultRsJwtService(IOptions<JwtConfigure> options)
        {
            this.options = options.Value;
        }

        public JwtTokenModel GetToken(IEnumerable<Claim> claims)
        {
            string keyDir = Directory.GetCurrentDirectory();
            if (!RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters rSAParameters))
            {
                throw new Exception($"指定文件夹下：{keyDir}没有私钥key.json");
            }

            var key = new RsaSecurityKey(rSAParameters);

            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: options.IssUser,
                audience: options.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(options.Expires),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature)
                ));
            return new JwtTokenModel
            {
                Token = token
            };
        }
    }
}
