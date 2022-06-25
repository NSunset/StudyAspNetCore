using Nw.RabbitMQ.Tool.Init;
using Nw.RabbitMQ.Tool.Interface;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Tool
{
    public class DefaultRabbitMQHandler : RabbitMQBase
    {
        public DefaultRabbitMQHandler(RabbitMQConnectionConfig config) : base(config)
        {

        }

    }
}
