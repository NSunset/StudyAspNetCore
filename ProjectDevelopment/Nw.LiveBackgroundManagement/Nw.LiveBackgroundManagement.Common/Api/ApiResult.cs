using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common.Api
{
    public class ApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success
        {
            get
            {
                return string.IsNullOrWhiteSpace(ErrorMessage);
            }
        }

        /// <summary>
        /// 特指错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }


        public static ApiResult Ok()
        {
            return Ok<string>(null);
        }

        public static ApiResult Ok<T>(T obj)
        {
            return Ok(obj, StatusCodes.Status200OK);
        }

        public static ApiResult Ok<T>(T obj, int code)
        {
            return new ApiResult
            {
                Code = code,
                Data = obj
            };
        }

        public static ApiResult Error(string message, int code)
        {
            return new ApiResult
            {
                Code = code,
                ErrorMessage = message
            };
        }

        /// <summary>
        /// 默认是200
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResult Error(string message)
        {
            return Error(message, StatusCodes.Status200OK);
        }

    }
}
