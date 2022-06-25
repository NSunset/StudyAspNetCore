using Consul;
using Microsoft.Extensions.Logging;
using Order.MicroService.Common;
using Order.MicroService.IService;
using Polly;
using Polly.Timeout;
using Polly.Wrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Order.MicroService.Service
{
    public class AccountServiceImpl : IAccountService
    {
        private AsyncPolicyWrap<bool> _policyWrap;
        private ILogger<AccountServiceImpl> _logger;

        public AccountServiceImpl(ILogger<AccountServiceImpl> logger)
        {
            _logger = logger;
            // 调用超时策略
            var timeout = Polly.Policy
                  // 超过一秒钟，就设定超时
                  .TimeoutAsync(1, TimeoutStrategy.Pessimistic, (context, ts, task) =>
                  {
                      _logger.LogInformation("AccountServiceImpl调用超时");
                      return Task.CompletedTask;
                  });
            // 调用熔断策略，对应服务注册方式必须是单例，其他方式都不行
            var circuitBreakerPolicy = Polly.Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(
                  exceptionsAllowedBeforeBreaking: 4,             // 连续4次异常
                  durationOfBreak: TimeSpan.FromMilliseconds(10),       // 断开10秒钟
                  onBreak: (exception, breakDelay) =>
                  {
                      //熔断以后，需要处理的动作；  记录日志；
                      _logger.LogInformation($"AccountServiceImpl服务出现=========>熔断");
                      _logger.LogInformation($"熔断: {breakDelay.TotalMilliseconds } ms, 异常: " + exception.Message);
                  },
                  onReset: () => //// 熔断器关闭时
                  {
                      _logger.LogInformation($"AccountServiceImpl服务熔断器关闭了");
                  },
                  onHalfOpen: () => // 熔断时间结束时，从断开状态进入半开状态
                  {
                      _logger.LogInformation($"AccountServiceImpl服务熔断时间到，进入半开状态");
                  });

            //服务降级，只要服务调不通，不是超时，也没有熔断。那就降级
            _policyWrap = Policy<bool>
                .Handle<Exception>()
                .FallbackAsync(AccountServiceFallback(), (x) =>
                {
                    _logger.LogInformation($"AccountService进行了服务降级 -- {x.Exception.Message}");
                    return Task.CompletedTask;
                })
                .WrapAsync(circuitBreakerPolicy)
                .WrapAsync(timeout);
        }

        public bool AccountServiceFallback()
        {
            return false;
        }

        public async Task<bool> DeductMoney(long? userId, decimal? money)
        {
            return await _policyWrap.ExecuteAsync(async () =>
            {
                //获取随机负载到服务实例
                AgentService agentService = await ConsulBalanceHelper.ChooseService("AccountService");
                string url = $"http://{agentService.Address}:{agentService.Port}/api/account/decrease/{userId}/{money}";
                ApiResponse apiResponse = HttpHelper.HttpRequest<ApiResponse>(url, HttpMethod.Get, null);
                return apiResponse.Success;
            });
        }

        public async Task<bool> DeductMoney1(long? userId, decimal? money)
        {
            // 获取随机负载到服务实例
            AgentService agentService = await ConsulBalanceHelper.ChooseService("AccountService");
            string url = $"http://{agentService.Address}:{agentService.Port}/api/account/decrease/{userId}/{money}";

            //string url = $"http://{"192.168.1.2"}:{"7000"}/api/account/decrease/{userId}/{money}";
            ApiResponse apiResponse = HttpHelper.HttpRequest<ApiResponse>(url, HttpMethod.Get, null);
            return apiResponse.Success;

        }
    }
}
