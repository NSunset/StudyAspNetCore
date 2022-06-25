using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Tool.Models
{
    public class RabbitMQEqeue
    {
        public string QueueName { get; set; }

        public string Key { get; set; } = "";

        public IDictionary<string, object> BindArguments { get; set; } = null;
    }
}
