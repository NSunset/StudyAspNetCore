using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Nw.MvcClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nw.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //退出当前系统
            //await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme); //Identityser4登录
            //return SignOut("Cookies", "OpenIdConnect");
            return SignOut("Cookies", "oidc");
        }

        public async Task<IActionResult> ApiResource()
        {
            string token = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            HttpClient client = new HttpClient();
            client.SetBearerToken(token);


            HttpResponseMessage response = await client.GetAsync("https://localhost:5003/api/Identity/Get");

            if (!response.IsSuccessStatusCode)
            {
                //如果是没有权限，就是说token过期了
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //刷新token
                    await RenewTokensAsync();
                    return RedirectToAction();
                }
                throw new Exception(response.ReasonPhrase);
            }
            object msg = await response.Content.ReadAsStringAsync();
            return View(msg);
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <returns></returns>
        private async Task<string> RenewTokensAsync()
        {
            HttpClient client = new HttpClient();
            DiscoveryDocumentResponse dico = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (dico.IsError)
            {
                throw new Exception(dico.Error);
            }

            string reToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            TokenResponse tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = dico.TokenEndpoint,
                ClientId = "mvc",
                ClientSecret = "secret",
                GrantType = OpenIdConnectGrantTypes.RefreshToken,
                RefreshToken = reToken
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            DateTime expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn);

            AuthenticationToken[] tokens = new[]
             {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.IdToken,
                    Value=tokenResponse.IdentityToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value=tokenResponse.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value=tokenResponse.RefreshToken
                },
                new AuthenticationToken
                {
                    Name="expires_at",
                    Value=expiresAt.ToString("o",CultureInfo.InvariantCulture)
                }
            };

            //获取身份认证的结果，包含当前的principal和properties
            AuthenticateResult authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //把新的token存起来
            authenticateResult.Properties.StoreTokens(tokens);

            //登录
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticateResult.Principal, authenticateResult.Properties);

            return tokenResponse.AccessToken;
        }


    }
}
