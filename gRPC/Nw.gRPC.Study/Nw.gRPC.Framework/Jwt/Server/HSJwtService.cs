
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nw.gRPC.Framework
{
    public class HSJWTService : IJwtService
    {
        private JwtConfigure _jwtConfigure;
        public HSJWTService(JwtConfigure options)
        {
            _jwtConfigure = options;
        }
        public string GetToken(UserDto userDto)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(nameof(userDto.Id),userDto.Id.ToString()),
                new Claim(nameof(userDto.Name),userDto.Name)
            };

            //使用对称可逆加密方式
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigure.SecurityKey));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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
