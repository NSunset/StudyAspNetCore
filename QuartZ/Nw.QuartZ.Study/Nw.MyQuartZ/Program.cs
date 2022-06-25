using Nw.QuartZ.Manager;
using System;
using System.Threading.Tasks;

namespace Nw.MyQuartZ
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                QuartzEntrance.Init();

                Console.ReadLine();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
