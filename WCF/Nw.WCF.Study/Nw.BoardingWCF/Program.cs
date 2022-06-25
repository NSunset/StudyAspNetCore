using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.BoardingWCF
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //使用控制台寄宿WCF
                ServiceInit.Process();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
