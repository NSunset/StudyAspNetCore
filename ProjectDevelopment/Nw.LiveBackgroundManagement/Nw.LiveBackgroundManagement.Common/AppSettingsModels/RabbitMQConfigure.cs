using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common
{
    public class RabbitMQConfigure
    {
        public const string Key = "RabbitMQConnections";

        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// 异常重试次数
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string SubscriptionClientName { get; set; }
    }
}
