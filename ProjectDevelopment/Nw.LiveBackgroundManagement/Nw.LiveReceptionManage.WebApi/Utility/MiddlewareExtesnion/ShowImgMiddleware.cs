using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.WebApi.Utility.MiddlewareExtesnion
{
    public static class ShowImgMiddleware
    {
        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder ShowImg(this IApplicationBuilder app, IConfiguration configuration)
        {
            var configfileAddress = configuration.GetSection("FileAddress");

            string path = configfileAddress.Value.TrimStart('.');
            return app.Map(path, builder =>
            {
                builder.Run(async context =>
                {
                    string url = $"{configfileAddress.Value}{context.Request.Path.Value}";

                    await context.Response.SendFileAsync(url.TrimStart('/'));

                });
            });
        }
    }
}
