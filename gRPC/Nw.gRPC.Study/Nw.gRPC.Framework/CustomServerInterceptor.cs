using Grpc.Core;
using Grpc.Core.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.gRPC.Framework
{
    /// <summary>
    /// GRPC服务端AOP
    /// 1、添加包：Grpc.Core.Api
    /// 2、继承Interceptor（来自于Grpc.Core.Interceptors）
    /// 3、复写UnaryServerHandler
    /// 4、让其生效：在服务端的配置中AddGrpc方法中加上options.Interceptors.Add<CustomServerInterceptor>();
    /// </summary>
    public class CustomServerInterceptor : Interceptor
    {
        /// <summary>
        /// 普通GRPC服务的AOP
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            //在执行服务前执行操作
            Aop();

            return continuation(request, context);
        }

        private void Aop()
        {
            Console.WriteLine("执行服务前执行");
        }
    }
}
