using Nw.ServiceWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Nw.BoardingWCF
{
    public class ServiceInit
    {
        public static void Process()
        {
            List<ServiceHost> serviceHosts = new List<ServiceHost>()
            {
                new ServiceHost(typeof(UserService))
            };

            foreach (ServiceHost host in serviceHosts)
            {
                host.Opening += (object sender, EventArgs e)=> 
                {
                    Console.WriteLine($"{host.GetType().Name}准备打开");
                };

                host.Opened += (object sender, EventArgs e) =>
                {
                     Console.WriteLine($"{host.GetType().Name}已经正常打开");
                };

                host.Closing+= (object sender, EventArgs e) =>
                {
                    Console.WriteLine($"{host.GetType().Name}准备关闭");
                };
                host.Closed += (object sender, EventArgs e) =>
                {
                    Console.WriteLine($"{host.GetType().Name}已经关闭");
                };
                host.Open();
            }

            Console.WriteLine("输入任何字符，就停止");
            Console.Read();

            foreach (ServiceHost host in serviceHosts)
            {
                host.Close();
            }

            Console.Read();
        }

        private static void Host_Opening(object sender, EventArgs e)
        {
        }
    }
}
