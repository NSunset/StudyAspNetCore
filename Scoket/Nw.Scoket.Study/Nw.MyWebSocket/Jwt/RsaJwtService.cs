using Nw.MyWebSocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Nw.MyWebSocket.Jwt
{
    public class RsaJwtService : IJwtService
    {
        private JwtConfigure _jwtConfigure;
        public RsaJwtService(JwtConfigure jwtConfigure)
        {
            _jwtConfigure = jwtConfigure;
        }

        public string GetToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(nameof(user.Id),user.Id.ToString()),
                new Claim(nameof(user.Name),user.Name.ToString()),
                new Claim(nameof(user.Age),user.Age.ToString()),
            };

            bool getRsa = RSAHelper.TryGetKeyParameters(_jwtConfigure.RsaPrivateKeyPath, true, out RSA rSA);

            if (!getRsa)
            {
                throw new Exception("登录获取RSA私钥文件错误，请查看");
            }

            SigningCredentials credentials = new SigningCredentials(new RsaSecurityKey(rSA), SecurityAlgorithms.RsaSha256Signature);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _jwtConfigure.Issuer,
                audience: _jwtConfigure.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfigure.Expires),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
