using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common.Api
{

    public class ApiPageResult<T> : Page where T : class
    {
        public List<T> DataList
        {
            get; set;
        } 
    }
}
