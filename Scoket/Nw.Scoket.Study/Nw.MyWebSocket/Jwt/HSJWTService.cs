using Nw.MyWebSocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Nw.MyWebSocket.Jwt
{
    public class HSJWTService : IJwtService
    {
        private JwtConfigure _jwtConfigure;
        public HSJWTService(JwtConfigure jwtConfigure)
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

            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigure.SecurityKey)), SecurityAlgorithms.HmacSha256);


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
