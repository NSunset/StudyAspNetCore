using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Common.CacheHelper;
using Nw.LiveBackgroundManagement.Common.DateHelper;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using RedisHelper.Service;

namespace Nw.LiveBackgroundManagement.Business.Interface.AopExtension
{
    /// <summary>
    /// 记录日志
    /// </summary>
    public class CustomAutofacStatisticsAop : IInterceptor
    {
        private readonly ILogger<CustomAutofacStatisticsAop> _logger;
        private readonly RedisZSetService _RedisZSetService;

        public CustomAutofacStatisticsAop(ILogger<CustomAutofacStatisticsAop> logger,
            RedisZSetService redisZSetService)
        {
            this._logger = logger;
            this._RedisZSetService = redisZSetService;
        }
        public void Intercept(IInvocation invocation)
        {
            _logger.LogInformation("开始获取排行数据");
            string key = string.Empty;
            if (invocation.Method.Name != nameof(ICSStatisticsService.TopTen))
            {
                invocation.Proceed();
                return;
            }
            switch ((RankingEnum)invocation.Arguments[0])
            {
                case RankingEnum.DailyRanking:
                    key = CacheKeyConstant.GetDaydataKeyConstant();
                    break;
                case RankingEnum.WeeklyRanking:
                    int weekOfYear = DateHelper.WeekOfYear(DateTime.Now.Day);
                    key = CacheKeyConstant.GetWeekdataKeyConstant(weekOfYear.ToString());
                    break;
                case RankingEnum.MonthlyRanking:
                    key = CacheKeyConstant.GetMonthdataKeyConstant();
                    break;
                case RankingEnum.RankingYear:
                    break;
                default:
                    break;

            }
            invocation.ReturnValue = this.GetStatisticsFromRedis(key, () =>
            {
                invocation.Proceed();
                return (List<StatisticsViewModel>)invocation.ReturnValue;
            });
        }


        /// <summary>
        /// 获取Redis的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private List<StatisticsViewModel> GetStatisticsFromRedis(string key, Func<List<StatisticsViewModel>> func)
        {
            var data = _RedisZSetService.SortedSetRangeByRankWithScores(key);
            if (data == null || data.Length == 0)
            {
                List<StatisticsViewModel> statisticsList = func.Invoke();
                foreach (var statistic in statisticsList)
                {
                    _RedisZSetService.SortedSetAdd(key, statistic.Ranking, statistic.Popularity);
                }
                return statisticsList;
            }
            else
            {
                return data.Select(item => new StatisticsViewModel { Ranking = item.Element, Popularity = item.Score }).ToList();
            }
        }

    }
}
