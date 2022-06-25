using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common.DateHelper
{
    public class DateHelper
    {
        /// <summary>
        /// 计算当天所在周是一年中的第几周
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int WeekOfYear(int day)
        {
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            return gc.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
