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
    public class GRPCController : ControllerBase
    {
        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            #region 客户端调用grpc说明
            //请求GRPC服务:
            //Grpc.Net.Client：客户端调用服务端的程序包
            //Google.Protobuf：类似于json/xml序列化，来传输数据
            //Grpc.Tools：让.proto文件生成c#代码

            //1、添加Grpc.Net.Client，Google.Protobuf，Grpc.Tools包
            //2、把服务端的Protos文件内容复制到客户端
            //3、在项目csproj文件中添加节点<ItemGroup><Protobuf Include = "Protos\greet.proto" GrpcServices = "Client" /></ItemGroup >
            //4、创建管道GrpcChannel.ForAddress("https://localhost:5001")
            //5、实例化服务Greeter.GreeterClient
            //6、调用服务

            #endregion


            using (GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                Greeter.GreeterClient client = new Greeter.GreeterClient(channel);

                HelloReply reply= await client.SayHelloAsync(new HelloRequest
                {
                    Name = "张三"
                });
                return new JsonResult(reply);
            }
        }
    }
}
