using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.ViewModel;

namespace Nw.LiveReceptionManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        private readonly ICSStatisticsService _ICSStatisticsService = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iCSStatisticsService"></param>
        public StatisticsController(ICSStatisticsService iCSStatisticsService)
        {
            this._ICSStatisticsService = iCSStatisticsService;
        }

        /// <summary>
        /// 日排行
        /// </summary>
        /// <returns></returns>
        [Route("RankingDay")]
        [HttpGet] 
        public List<StatisticsViewModel> RankingDay()
        {
            List<StatisticsViewModel> RankingDayList = _ICSStatisticsService.TopTen(RankingEnum.DailyRanking);
            return RankingDayList;
        }

        /// <summary>
        /// 年排行
        /// </summary>
        /// <returns></returns>
        [Route("RankingYear")]
        [HttpGet] 
        public List<StatisticsViewModel> RankingYear()
        {
            List<StatisticsViewModel> RankingYearList = _ICSStatisticsService.TopTen(RankingEnum.DailyRanking);
            return RankingYearList;
        }

        /// <summary>
        /// 周排行
        /// </summary>
        /// <returns></returns>
        [Route("RankingWeek")]
        [HttpGet]
        public List<StatisticsViewModel> RankingWeek()
        {
            List<StatisticsViewModel> RankingWeekList = _ICSStatisticsService.TopTen(RankingEnum.DailyRanking);
            return RankingWeekList;
        }

        /// <summary>
        /// 月排行
        /// </summary>
        /// <returns></returns>
        [Route("RankingMonth")]
        [HttpGet]
        public List<StatisticsViewModel> RankingMonth()
        {
            List<StatisticsViewModel> RankingMonthList = _ICSStatisticsService.TopTen(RankingEnum.DailyRanking);
            return RankingMonthList;
        } 
    }
}
