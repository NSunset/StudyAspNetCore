using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Tool
{
    public class PublishMsg
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 发送消息指定Key：当交换机类型是
        /// 1、Direct的时候，BindKey和PublishKey相同
        /// 2、Topic的时候，BindKey和PublishKey都需要
        /// 3、Fanout和Headers不需要BindKey和PublishKey
        /// </summary>
        public string PublishKey { get; set; } = null;

        /// <summary>
        /// 除非使用的交换机类型是Headers，否则一般不设置
        /// </summary>
        public Dictionary<string, object> PublishHeaders { get; set; } = null;


        public PublishMsg() { }

        /// <summary>
        /// 消息实体
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        /// <param name="headers"></param>
        public PublishMsg(string exchangeName, string msg, string key = null, Dictionary<string, object> headers = null)
        {
            this.ExchangeName = exchangeName;
            this.Message = msg;
            this.PublishKey = key;
            this.PublishHeaders = headers;
        }
    }
}
