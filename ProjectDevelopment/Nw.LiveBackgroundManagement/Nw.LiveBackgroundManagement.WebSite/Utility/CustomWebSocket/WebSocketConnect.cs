using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Nw.LiveBackgroundManagement.Common;
using Autofac;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Microsoft.Extensions.Logging;

namespace Nw.LiveBackgroundManagement.WebSite.Utility.CustomWebSocket
{

    public class WebSocketConnect
    {
        private readonly RequestDelegate _next;
        private ILogger<WebSocketConnect> _logger;
        public WebSocketConnect(
            RequestDelegate next,
            ILogger<WebSocketConnect> logger
            )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context
            )
        {
            await Acceptor(context);
            await _next(context);
        }

        /// <summary>
        /// 创建WebSocket链接
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private async Task Acceptor(HttpContext httpContext)
        {
            if (!httpContext.WebSockets.IsWebSocketRequest)//如果是WebSocket链接
            {
                return;
            }
            WebSocket socket = await httpContext.WebSockets.AcceptWebSocketAsync();
            while (true) //每间隔3秒钟就获取一次数据发送到前端的Html页
            {
                //当前日：
                IEnumerable<object> daydata = null;
                //本周：
                IEnumerable<object> weekdata = null;
                //当前月份：  
                IEnumerable<object> monthdata = null;

                try
                {
                    using (ICSStatisticsService cSStatisticsService = httpContext.RequestServices.GetService(typeof(ICSStatisticsService)) as ICSStatisticsService)
                    {
                        //这里再定义Key的时候，建议大家写一个方法去获取
                        {
                            //在这里是直接取Reis； 应该先取Redis，如果取不到数据，可能是数据已经过期了；还要到数据库总去取；
                            daydata = cSStatisticsService
                                .TopTen(RankingEnum.DailyRanking)
                                .Select(item => new
                                {
                                    ranking = item.Ranking,
                                    popularity = item.Popularity
                                });

                        }
                        {
                            weekdata = cSStatisticsService
                                .TopTen(RankingEnum.WeeklyRanking)
                                .Select(item => new
                                {
                                    ranking = item.Ranking,
                                    popularity = item.Popularity
                                });
                        }
                        {
                            monthdata = cSStatisticsService
                                .TopTen(RankingEnum.MonthlyRanking)
                                .Select(item => new
                                {
                                    ranking = item.Ranking,
                                    popularity = item.Popularity
                                });
                        }
                    }


                    if (socket.State != WebSocketState.Closed)
                    {
                        object objResult = new
                        {
                            daydata = daydata,
                            weekdata = weekdata,
                            monthdata = monthdata
                        };
                        await SendAsync(JsonConvert.SerializeObject(objResult), socket);
                        Thread.Sleep(3000);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("WebSocket错误", ex);
                }
            }
        }

        /// <summary>
        /// 向客户端发送数据 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public async Task SendAsync(string msg, WebSocket webSocket)
        {
            try
            {
                //业务逻辑
                CancellationToken cancellation = default(CancellationToken);
                byte[] buf = Encoding.UTF8.GetBytes(msg);
                ArraySegment<byte> segment = new ArraySegment<byte>(buf);
                await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError("websocket报错", ex);
            }
        }

        /// <summary>
        /// 接收客户端数据
        /// </summary>
        /// <param name="webSocket">webSocket 对象</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> RecvAsync(WebSocket webSocket, CancellationToken cancellationToken)
        {
            WebSocketReceiveResult result;
            do
            {
                MemoryStream ms = new MemoryStream();
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024 * 8]);
                result = await webSocket.ReceiveAsync(buffer, cancellationToken);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
            } while (result.EndOfMessage);

            return "";
        }


        /// 路由绑定处理
        /// </summary>
        /// <param name="app"></param>
        public static void MapWebSocket(IApplicationBuilder app)
        {
            app.UseWebSockets();

            app.UseMiddleware<WebSocketConnect>();
            //app.Use(Acceptor);
        }
    }

}
