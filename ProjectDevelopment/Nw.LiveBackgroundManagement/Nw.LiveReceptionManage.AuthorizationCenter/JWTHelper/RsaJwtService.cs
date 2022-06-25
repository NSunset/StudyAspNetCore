using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Extensions.Options;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Nw.LiveBackgroundManagement.Common;
using Newtonsoft.Json;
using Nw.LiveBackgroundManagement.Common.JWTHelper;
using RedisHelper.Service;
using Microsoft.Extensions.Logging;

namespace Nw.LiveReceptionManage.AuthorizationCenter.JWTHelper
{
    public class RsaJwtService : IJwtService
    {
        private JwtConfigure _jwtConfigure;
        private RedisStringService _redisStringService;
        private ILogger<RsaJwtService> _logger;

        public RsaJwtService(
            IOptions<JwtConfigure> options, 
            RedisStringService redisStringService,
            ILogger<RsaJwtService> logger
            )
        {
            _jwtConfigure = options.Value;
            _redisStringService = redisStringService;
            _logger = logger;
        }

        public LoginResultViewModel GetToken(CSUser cSUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(nameof(cSUser.Id),cSUser.Id.ToString()),
                new Claim(nameof(cSUser.Name),cSUser.Name)
            };
            bool getKey = RsaHelper.TryGetKeyParameters(_jwtConfigure.RsaPrivateKeyPath, true, out RSA rSA);

            if (!getKey)
            {
                _logger.LogError($"读取jwt秘钥文件失败：{_jwtConfigure.RsaPrivateKeyPath}");
                return null;
            }
            RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(rSA);

            SigningCredentials credentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256Signature);


            var hand = new JwtSecurityTokenHandler();

            JwtSecurityToken accessToken = new JwtSecurityToken(
                issuer: _jwtConfigure.Issuer,
                audience: _jwtConfigure.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfigure.AccessTokenExpires),
                signingCredentials: credentials
                );
            JwtSecurityToken refreshToken = new JwtSecurityToken(
                issuer: _jwtConfigure.Issuer,
                audience: _jwtConfigure.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfigure.RefreshTokenExpires),
                signingCredentials: credentials
                );

            LoginResultViewModel loginResult = new LoginResultViewModel
            {
                AccessToken = hand.WriteToken(accessToken),
                RefreshToken = hand.WriteToken(refreshToken),
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
