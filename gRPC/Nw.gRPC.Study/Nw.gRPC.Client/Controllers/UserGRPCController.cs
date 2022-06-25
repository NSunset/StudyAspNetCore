using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
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
    public class UserGRPCController : ControllerBase
    {
        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            using (GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                UserGrpc.UserGrpcClient client = new UserGrpc.UserGrpcClient(channel);

                GetRequest request = new GetRequest
                {
                    Id = 10,
                    Name = "张三"
                };
                GetReply response = await client.GetUserAsync(request);

                return new JsonResult(response);
            }
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            using (GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                UserGrpc.UserGrpcClient client = new UserGrpc.UserGrpcClient(channel);

                GetReply response = await client.GetAllAsync(new Empty());

                return new JsonResult(response);
            }
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(User user)
        {
            using (GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                UserGrpc.UserGrpcClient client = new UserGrpc.UserGrpcClient(channel);

                Empty response = await client.AddAsync(user);

                return new JsonResult(response);
            }
        }

        [Route("SelfIncreaseClientTest")]
        [HttpGet]
        public async Task<IActionResult> SelfIncreaseClientTest()
        {
            try
            {
                using (GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001"))
                {
                    UserGrpc.UserGrpcClient client = new UserGrpc.UserGrpcClient(channel);

                    AsyncClientStreamingCall<BathTheCatReq, IntArrayModel> call = client.SelfIncreaseClient();

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
            using (GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                UserGrpc.UserGrpcClient client = new UserGrpc.UserGrpcClient(channel);

                IntArrayModel request = new IntArrayModel();
                for (int i = 0; i < 10; i++)
                {
                    request.Number.Add(new Random().Next(0,40));
                }

                //中断连接只能由客户端发起
                System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(2));//2秒后取消线程

                AsyncServerStreamingCall<BathTheCatResp> call = client.SelfIncreaseServer(request,cancellationToken: cts.Token);//指定客户端2秒后断开连接

                List<string> msgs = new List<string>();
                while (await call.ResponseStream.MoveNext())
                {
                    string msg = call.ResponseStream.Current.Message;
                    Console.WriteLine(msg);
                    msgs.Add(msg);
                }
                return new JsonResult(msgs);
            }
        }

        [Route("SelfIncreaseDoubleTest")]
        [HttpGet]
        public async Task<IActionResult> SelfIncreaseDoubleTest()
        {
            using (GrpcChannel channel=GrpcChannel.ForAddress("https://localhost:5001"))
            {
                UserGrpc.UserGrpcClient client = new UserGrpc.UserGrpcClient(channel);

                AsyncDuplexStreamingCall<BathTheCatReq, BathTheCatResp> call = client.SelfIncreaseDouble();


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
}
