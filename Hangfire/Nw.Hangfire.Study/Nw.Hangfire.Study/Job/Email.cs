using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nw.Hangfire.Study
{
    public class Email
    {
        public void Send(int userId, string message)
        {
            Console.WriteLine("发送邮件");
        }
    }
}
