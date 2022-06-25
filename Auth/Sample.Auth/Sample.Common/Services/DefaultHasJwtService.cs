using Microsoft.Extensions.Options;
using Sample.Common.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Sample.Common.Services
{
    public class DefaultHasJwtService : IJwtService
    {
        private readonly JwtConfigure options;

        public DefaultHasJwtService(IOptions<JwtConfigure> options)
        {
            this.options = options.Value;
        }

        public JwtTokenModel GetToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));

            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                 issuer: options.IssUser,
                 audience: options.Audience,
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(options.Expires),
                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                 ));

            return new JwtTokenModel
            {
                Token = token
            };
        }
    }
}
