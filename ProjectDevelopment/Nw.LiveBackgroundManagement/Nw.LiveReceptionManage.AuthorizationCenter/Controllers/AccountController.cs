using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Nw.LiveReceptionManage.AuthorizationCenter.JWTHelper;
using RedisHelper.Service;

namespace ZhaoXi.LiveReceptionManagement.AuthorizationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IJwtService _iJWTService = null;
        private ICSUserservice _iCSUserservice = null;
        private readonly IConfiguration _iConfiguration;
        private readonly RedisStringService _RedisStringService = null;
        public AccountController(
            IConfiguration configuration,
            IJwtService service,
            ICSUserservice iCSUserservice,
            RedisStringService redisStringService)
        {
            _iConfiguration = configuration;
            _iJWTService = service;
            _iCSUserservice = iCSUserservice;
            _RedisStringService = redisStringService;
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [Route("VerifyCodeImg")]
        [HttpGet]
        public IActionResult VerifyCodeImg(string tag)
        {
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out string code);
            _RedisStringService.Set(LoginModel.GetVerifyCodeKey(tag), code, TimeSpan.FromSeconds(30));
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");//返回FileContentResult图片
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            ApiResult apiResult = new ApiResult();
            string redisResult = _RedisStringService.Get(
                LoginModel.GetVerifyCodeKey(loginModel.Tag));
            if (string.IsNullOrWhiteSpace(redisResult))
            {
                apiResult.ErrorMessage = "请重新刷新验证码";
                return new JsonResult(apiResult);
            };
            string equesString = redisResult.Replace("\"", "");
            if (!string.Equals(equesString, loginModel.ImgCode, StringComparison.CurrentCultureIgnoreCase))
            {
                apiResult.ErrorMessage = "验证码输入错误";
                return new JsonResult(apiResult);
            }
            CSUser cSUser = null;
            bool bResult = _iCSUserservice.Login(loginModel.Name, loginModel.Password, out cSUser);
            if (bResult)
            {
                LoginResultViewModel token = this._iJWTService.GetToken(cSUser);
                return new JsonResult(new ApiResult()
                {
                    Data = token
                });
            }
            else
            {
                return new JsonResult(new ApiResult()
                {
                    ErrorMessage = "用户名或者密码错误"
                }); ;
            }
        }

        [Route("RefreshToken")]
        [HttpPost]
        public IActionResult RefreshToken(string refreshToken)
        {
            ApiResult apiResult = new ApiResult();
            CSUser cSUser = LoginResultViewModel.GetCacheRefreshToken(_RedisStringService, refreshToken);
            if (cSUser == null)
            {
                apiResult.ErrorMessage = "token已过期，请重新登录";
                return new JsonResult(apiResult);
            };
            LoginResultViewModel token = this._iJWTService.RefreshToken(cSUser);
            if (token != null)
            {
                return new JsonResult(new ApiResult()
                {
                    Data = token
                });
            }
            else
            {
                return new JsonResult(new ApiResult()
                {
                    ErrorMessage = "请重新登录"
                }); ;
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="cSUserViewModel"></param>
        /// 要注意非法注册
        /// <returns></returns>
        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            var result = _iCSUserservice.Register(registerModel);
            return new JsonResult(result);
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [Route("SendVerifyCode")]
        [HttpPost]
        public JsonResult SendVerifyCode()
        {
            //检查的时候，需要 ip--
            if (HttpContext.Request.Form.Keys.Count == 0)
            {
                return new JsonResult(new ApiResult()
                {
                    ErrorMessage = "请输入手机号"
                });
            }
            var mobile = HttpContext.Request.Form.Keys.FirstOrDefault();
            ApiResult ajaxResult = this._iCSUserservice.CheckPhoneNumberBeforeSend(mobile);
            if (!ajaxResult.Success)//校验失败
            {
                return new JsonResult(ajaxResult);
            }
            else
            {
                var result = _iCSUserservice.SendVerifyCode(mobile);
                return new JsonResult(result);
            }
        }
    }
}
