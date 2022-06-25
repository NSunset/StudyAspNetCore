using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Nw.LiveBackgroundManagement.Models.CSEnum;

namespace Nw.LiveBackgroundManagement.WebSite.Controllers
{
    public class RankingListController : Controller
    {

        private readonly ICSStatisticsService _ICSStatisticsService = null;

        public RankingListController(ICSStatisticsService iCSStatisticsService)
        {
            this._ICSStatisticsService = iCSStatisticsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Statistics()
        {
            List<StatisticsViewModel> daylist = _ICSStatisticsService.TopTen(RankingEnum.DailyRanking);
            List<StatisticsViewModel> weeklist = _ICSStatisticsService.TopTen(RankingEnum.WeeklyRanking);
            List<StatisticsViewModel> monthlist = _ICSStatisticsService.TopTen(RankingEnum.MonthlyRanking);
            return new JsonResult(new
            {
                daydata = daylist,
                weekdata = weeklist,
                monthdata = monthlist
            });
        }
    }
}
