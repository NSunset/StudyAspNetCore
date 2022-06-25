using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nw.gRPC.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.gRPC.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserV2Controller : ControllerBase
    {
        /// <summary>
        /// 要实现依赖注入需要添加包：Grpc.Net.ClientFactory
        /// </summary>
        private readonly UserGrpc.UserGrpcClient _userGrpcClient;
        public UserV2Controller(UserGrpc.UserGrpcClient userGrpcClient)
        {
            _userGrpcClient = userGrpcClient;
        }

        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            GetRequest request = new GetRequest
            {
                Id = 10,
                Name = "张三"
            };
            //传递token的一种方式
            //Metadata entries = new Metadata();
            //entries.Add("Authorization", $"Bearer {token}");
            //GetReply response = await _userGrpcClient.GetUserAsync(request,headers: entries);
            GetReply response = await _userGrpcClient.GetUserAsync(request);

            return new JsonResult(response);
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            GetReply response = await _userGrpcClient.GetAllAsync(new Empty());

            return new JsonResult(response);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(User user)
        {
            Empty response = await _userGrpcClient.AddAsync(user);

            return new JsonResult(response);
        }

        [Route("SelfIncreaseClientTest")]
        [HttpGet]
        public async Task<IActionResult> SelfIncreaseClientTest()
        {
            try
            {
                AsyncClientStreamingCall<BathTheCatReq, IntArrayModel> call = _userGrpcClient.SelfIncreaseClient();

                for (int i = 0; i < 10; i++)
                {
                    await call.RequestStream.WriteAsync(new BathTheCatReq
                    {
                        Id = new Random().Next(0, 30)
                    });
                    System.Threading.Thread.Sleep(500);
                }
                //等待客户端发送完成
                await call.RequestStream.CompleteAsync();

                //获取服务端响应数据
                IntArrayModel response = await call.ResponseAsync;

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [Route("SelfIncreaseServerTest")]
        [HttpGet]
        public async Task<IActionResult> SelfIncreaseServerTest()
        {
            IntArrayModel request = new IntArrayModel();
            for (int i = 0; i < 10; i++)
            {
                request.Number.Add(new Random().Next(0, 40));
            }

            //中断连接只能由客户端发起
            System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(2));//2秒后取消线程

            AsyncServerStreamingCall<BathTheCatResp> call = _userGrpcClient.SelfIncreaseServer(request, cancellationToken: cts.Token);//指定客户端2秒后断开连接

            List<string> msgs = new List<string>();
            while (await call.ResponseStream.MoveNext())
            {
                string msg = call.ResponseStream.Current.Message;
                Console.WriteLine(msg);
                msgs.Add(msg);
            }
            return new JsonResult(msgs);
        }

        [Route("SelfIncreaseDoubleTest")]
        [HttpGet]
        public async Task<IActionResult> SelfIncreaseDoubleTest()
        {
            ////中断连接只能由客户端发起
            //System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
            //cts.CancelAfter(TimeSpan.FromSeconds(2));//2秒后取消线程

            AsyncDuplexStreamingCall<BathTheCatReq, BathTheCatResp> call = _userGrpcClient.SelfIncreaseDouble(
                //cancellationToken:cts.Token
                );


            for (int i = 0; i < 10; i++)
            {
                await call.RequestStream.WriteAsync(new BathTheCatReq
                {
                    Id = new Random().Next(0, 30)
                });
            }

            await call.RequestStream.CompleteAsync();

            List<string> msgs = new List<string>();
            while (await call.ResponseStream.MoveNext())
            {
                string msg = call.ResponseStream.Current.Message;
                msgs.Add(msg);
                Console.WriteLine(msg);
            }

            return new JsonResult(msgs);
        }
    }
}
