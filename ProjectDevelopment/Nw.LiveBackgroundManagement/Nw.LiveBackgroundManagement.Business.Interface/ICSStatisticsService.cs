using Autofac.Extras.DynamicProxy;
using Nw.LiveBackgroundManagement.Business.Interface.AopExtension;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Business.Interface
{

    /// <summary>
    /// 
    /// </summary>
    [Intercept(typeof(CustomAutofacStatisticsAop))]
    public interface ICSStatisticsService : IBaseService
    {
        /// <summary>
        /// 排行前十
        /// </summary>
        /// <returns></returns>
        public List<StatisticsViewModel> TopTen(RankingEnum rankingEnum);
    }

}
