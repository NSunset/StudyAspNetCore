using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.gRPC.Server
{
    /// <summary>
    /// 创建GRPC服务：
    /// 1：添加.proto文件，在文件内部声明服务类，方法，请求返回对象
    /// 2：在项目.csproj文件中添加节点<Protobuf Include="Protos\User.proto" GrpcServices="Server" />
    /// 3：重新生成项目，看obj\Debug\net5.0\Protos目录下是否有生成对应的.cs文件。有就添加成功了
    /// 4：添加c#代码，UserService继承.proto文件中写的服务名.服务名Base
    /// 5：重写.proto文件中声明的方法
    /// 6：在配置中间件方法中添加endpoints.MapGrpcService<UserService>();
    /// </summary>
    public class UserService : UserGrpc.UserGrpcBase
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        //使用JWT鉴权
        [Authorize]
        public async override Task<GetReply> GetUser(GetRequest request, ServerCallContext context)
        {
            return await Task.FromResult(new GetReply
            {
                UserId = request.Id + 10,
                UserName = $"你好：{request.Name}"
            });
            //return base.GetUser(request, context); 
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        //使用JWT鉴权
        [Authorize]
        public override Task<GetReply> GetAll(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new GetReply
            {
                UserId = 10,
                UserName = "李四"
            });
            //return base.GetAll(request, context);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
         //使用JWT鉴权
        [Authorize]
        public override Task<Empty> Add(User request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
            //return base.Add(request, context);
        }

        /// <summary>
        /// 客户端分段传数据
        /// </summary>
        /// <param name="requestStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
         //使用JWT鉴权
        [Authorize]
        public async override Task<IntArrayModel> SelfIncreaseClient(IAsyncStreamReader<BathTheCatReq> requestStream, ServerCallContext context)
        {
            try
            {
                IntArrayModel intArrayModel = new IntArrayModel();

                while (await requestStream.MoveNext())
                {
                    Console.WriteLine($"拿到客户端数据：{requestStream.Current.Id}开始处理");
                    intArrayModel.Number.Add(requestStream.Current.Id);
                }

                return intArrayModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return base.SelfIncreaseClient(requestStream, context);
        }


        /// <summary>
        /// 服务端分段返回数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
         //使用JWT鉴权
        [Authorize]
        public async override Task SelfIncreaseServer(IntArrayModel request, IServerStreamWriter<BathTheCatResp> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < request.Number.Count; i++)
            {
                int number = request.Number[i];
                await responseStream.WriteAsync(new BathTheCatResp
                {
                    Message = $"服务端返回数据：你好啊,{number}"
                });

                System.Threading.Thread.Sleep(500);
            }
            //return base.SelfIncreaseServer(request, responseStream, context);
        }

        /// <summary>
        /// 双工
        /// </summary>
        /// <param name="requestStream"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
         //使用JWT鉴权
        [Authorize]
        public async override Task SelfIncreaseDouble(IAsyncStreamReader<BathTheCatReq> requestStream, IServerStreamWriter<BathTheCatResp> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                Console.WriteLine($"处理客户端传来数据：{requestStream.Current.Id}");

                await responseStream.WriteAsync(new BathTheCatResp
                {
                    Message = $"服务端返回结果数据：你好啊：{requestStream.Current.Id}"
                });
                System.Threading.Thread.Sleep(500);
            }
            //return base.SelfIncreaseDouble(requestStream, responseStream, context);
        }
    }
}
