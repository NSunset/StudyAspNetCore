using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.gRPC.Framework
{
    /// <summary>
    /// 支持GRPC客户端AOP
    /// </summary>
    public class CustomClientInterceptor : Interceptor
    {
        private readonly ILogger<CustomClientInterceptor> _logger;
        public CustomClientInterceptor(ILogger<CustomClientInterceptor> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 普通GRPC服务的AOP
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            //在执行服务前执行操作
            Aop();

            return continuation(request, context);
        }


        private void Aop()
        {
            Console.WriteLine("在请求Grpc服务前执行");
        }
    }
}
