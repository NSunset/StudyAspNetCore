using Microsoft.Extensions.Options;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Nw.LiveBackgroundManagement.Common.JWTHelper;
using RedisHelper.Service;

namespace Nw.LiveReceptionManage.AuthorizationCenter.JWTHelper
{
    public class HSJwtService : IJwtService
    {
        private JwtConfigure _jwtConfigure;
        private RedisStringService _redisStringService;
        public HSJwtService(IOptions<JwtConfigure> options, RedisStringService redisStringService)
        {
            _jwtConfigure = options.Value;
            _redisStringService = redisStringService;
        }
        public LoginResultViewModel GetToken(CSUser cSUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(nameof(cSUser.Id),cSUser.Id.ToString()),
                new Claim(nameof(cSUser.Name),cSUser.Name)
            };

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigure.SecurityKey));

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken accessToken = new JwtSecurityToken(
                issuer: _jwtConfigure.Issuer,
                audience: _jwtConfigure.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfigure.AccessTokenExpires),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );

            JwtSecurityToken refreshToken = new JwtSecurityToken(
                issuer: _jwtConfigure.Issuer,
                audience: _jwtConfigure.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfigure.RefreshTokenExpires),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );

            LoginResultViewModel loginResult = new LoginResultViewModel
            {
                AccessToken = handler.WriteToken(accessToken),
                RefreshToken = handler.WriteToken(refreshToken),
                UserId = cSUser.Id,
                UserName = cSUser.Name
            };
            loginResult.CacheRefreshToken(
                _redisStringService,
                cSUser,
                TimeSpan.FromMinutes(_jwtConfigure.RefreshTokenExpires)
                );

            return loginResult;
        }

        public LoginResultViewModel RefreshToken(CSUser cSUser)
        {
            return GetToken(cSUser);
        }

    }
}
