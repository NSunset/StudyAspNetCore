using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using System.Linq.Expressions;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using RedisHelper.Service;

namespace Nw.LiveBackgroundManagement.Business.Services
{
    public class CSStatisticsService : BaseService,ICSStatisticsService
    {
        private readonly RedisZSetService _RedisZSetService = null;

        public CSStatisticsService(AuthorityDbContext context, RedisZSetService redisZSetService) : base(context)
        {
            _RedisZSetService = redisZSetService;
        }

        /// <summary>
        /// 支持 日  周   月  年度最佳人气10个排行
        /// </summary>
        /// <param name="rankingEnum"></param>
        /// <returns></returns>
        public virtual List<StatisticsViewModel> TopTen(RankingEnum rankingEnum)
        {
            Expression<Func<CSScoreList, bool>> expression = null;
            switch (rankingEnum)
            {
                case RankingEnum.DailyRanking:
                    {
                        expression = c => c.CreateTime.Day == DateTime.Now.Day;
                    }
                    break;
                case RankingEnum.WeeklyRanking:
                    {
                        DateTime now = DateTime.Now;
                        DateTime startWeek = now.AddDays(1 - Convert.ToInt32(now.DayOfWeek.ToString("d")));
                        expression = c => c.CreateTime >= startWeek && c.CreateTime < now;
                    }
                    break;
                case RankingEnum.MonthlyRanking:
                    {
                        int month = DateTime.Now.Month;
                        expression = c => c.CreateTime.Month == month;
                    }
                    break;
                case RankingEnum.RankingYear:
                    {
                        int year = DateTime.Now.Year;
                        expression = c => c.CreateTime.Year == year;
                    }
                    break;
                default:
                    throw new Exception("暂不支持该排行");
            }

            return RankingOfPoints(expression);
        }

        /// <summary>
        /// 排行计算
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<StatisticsViewModel> RankingOfPoints(Expression<Func<CSScoreList, bool>> expression)
        {
            var list = _Context.Set<CSScoreList>()
             .Where(expression)
             .GroupBy(u => u.UserName)
             .Select(gropUser => new StatisticsViewModel
             {
                 Popularity = Convert.ToDouble(gropUser.Sum(x => x.Integral)),
                 Ranking = gropUser.Key.ToString()
             }).Take(10).ToList();

            return list;
        }
    }
}
