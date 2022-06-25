using Nw.RabbitMQ.Tool.Init;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Tool.Models
{
    public class RabbitMQExchange
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 交换机类型
        /// ExchangeType
        /// </summary>
        public string ExchangeType { get; set; }

        /// <summary>
        /// 多个需要绑定的队列
        /// </summary>
        public List<RabbitMQEqeue> RabbitMQEqeue { get; set; }
    }
}
