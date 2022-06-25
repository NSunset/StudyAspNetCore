
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nw.gRPC.Framework
{
    public class RsaJwtService : IJwtService
    {
        private JwtConfigure _jwtConfigure;
        public RsaJwtService(JwtConfigure jwtConfigure)
        {
            _jwtConfigure = jwtConfigure;
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public string GetToken(UserDto userDto)
        {
            Claim[] claims = new[]
            {
                new Claim(nameof(userDto.Id),userDto.Id.ToString()),
                new Claim(nameof(userDto.Name),userDto.Name)
            };

            bool getKey = RSAHelper.TryGetKeyParameters(_jwtConfigure.RsaPrivateKeyPath,true, out RSA rSA);

            if (!getKey)
                return "";

            SecurityKey securityKey= new RsaSecurityKey(rSA);

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
               issuer: _jwtConfigure.Issuer,
               audience: _jwtConfigure.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_jwtConfigure.Expires),
               signingCredentials: credentials
               );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
