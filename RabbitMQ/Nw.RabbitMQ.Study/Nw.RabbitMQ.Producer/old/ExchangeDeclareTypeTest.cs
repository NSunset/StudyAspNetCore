using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Nw.RabbitMQ.Producer.old
{
    /// <summary>
    /// 不同交换机类型发送消息测试
    /// </summary>
    public class ExchangeDeclareTypeTest
    {
        public static void Show()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();

            connectionFactory.UserName = "admin";
            connectionFactory.Password = "admin";
            connectionFactory.HostName = "192.168.157.128";
            connectionFactory.VirtualHost = "my_vhost";

            //创建连接
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                //创建信道
                IModel channel = connection.CreateModel();

                string queue1 = "Test_1";
                string queue2 = "Test_2";
                //创建一个队列
                channel.QueueDeclare(
                    queue: queue1,
                    //是否持久化
                    durable: true,
                    //连接关闭后，队列是否删除
                    exclusive: false,
                    //当最后一个消费者取消订阅时（如果有消费者）,是否自动删除队列
                    autoDelete: false,
                    arguments: null
                    );

                //创建一个队列
                channel.QueueDeclare(
                    queue: queue2,
                    //是否持久化
                    durable: true,
                    //连接关闭后，队列是否删除
                    exclusive: false,
                    //当最后一个消费者取消订阅时（如果有消费者）,是否自动删除队列
                    autoDelete: false,
                    arguments: null
                    );


                //交换机类型1：Direct
                {
                    ////所有消息发送到交换机指定的队列，用key来标识不同队列绑定

                    //声明两个不同的标识
                    //string routingKey1 = "routingKey1";
                    //string routingKey2 = "routingKey2";

                    ////直接交换Direct类型
                    //string exchangeDirect = "Nw_ExchangeDirect";
                    //channel.ExchangeDeclare(
                    //    exchange: exchangeDirect,
                    //    type: ExchangeType.Direct,
                    //    //是否持久化
                    //    durable: true,
                    //    //连接关闭后，是否删除
                    //    autoDelete: false,
                    //    arguments: null
                    //    );

                    ////绑定交换机exchangeDirect和队列queue1，指定标识为routingKey1
                    //channel.QueueBind(
                    //    queue: queue1,
                    //    exchange: exchangeDirect,
                    //    routingKey: routingKey1,
                    //    arguments: null
                    //    );

                    ////绑定交换机exchangeDirect和队列queue2，指定标识为routingKey2
                    //channel.QueueBind(
                    //    queue: queue2,
                    //    exchange: exchangeDirect,
                    //    routingKey: routingKey2,
                    //    arguments: null
                    //    );

                    //IBasicProperties basicProperties = channel.CreateBasicProperties();

                    //string msg = "你好吧，这里是发送消息";
                    //byte[] body = Encoding.UTF8.GetBytes(msg);

                    ////这里发送消息到队列1里面了。因为这里使用的标识routingKey1，绑定的是队列1
                    //channel.BasicPublish(
                    //    exchange: exchangeDirect,
                    //    routingKey: routingKey1,
                    //    basicProperties: basicProperties,
                    //    body: body
                    //    );
                    //Console.WriteLine($"发送消息:{msg}");

                    //string msg1 = "哈哈哈，你好";
                    //byte[] body1 = Encoding.UTF8.GetBytes(msg1);

                    ////这里发送消息到队列1里面了。因为这里使用的标识routingKey1，绑定的是队列1
                    //channel.BasicPublish(
                    //    exchange: exchangeDirect,
                    //    routingKey: routingKey2,
                    //    basicProperties: basicProperties,
                    //    body: body1
                    //    );
                    //Console.WriteLine($"发送消息:{msg1}");
                }

                //交换机类型2：Fanout
                {
                    ////消息发送到交换机绑定的所有队列
                    ////一个生产者发布的消息，可以让多个消费者都接收到。实现发布订阅
                    //string exchangeFanout = "exchangeFanout";

                    //channel.ExchangeDeclare(
                    //    exchange: exchangeFanout,
                    //    type: ExchangeType.Fanout,
                    //    //是否持久化
                    //    durable: true,
                    //    //是否在断开链接时，删除
                    //    autoDelete: false,
                    //    arguments: null
                    //    );

                    ////绑定队列1
                    //channel.QueueBind(
                    //    queue: queue1,
                    //    exchange: exchangeFanout,
                    //    routingKey: "",
                    //    arguments: null
                    //    );

                    ////绑定队列2
                    //channel.QueueBind(
                    //   queue: queue2,
                    //   exchange: exchangeFanout,
                    //   routingKey: "",
                    //   arguments: null
                    //   );

                    //IBasicProperties properties = channel.CreateBasicProperties();

                    //string msg = "Hello World";
                    //byte[] body = Encoding.UTF8.GetBytes(msg);

                    //channel.BasicPublish(
                    //  exchange: exchangeFanout,
                    //  routingKey: "",
                    //  basicProperties: properties,
                    //  body: body
                    //  );

                    //Console.WriteLine($"发送消息{msg}");

                    //channel.BasicPublish(
                    //  exchange: exchangeFanout,
                    //  routingKey: "",
                    //  basicProperties: properties,
                    //  body: body
                    //  );

                    //Console.WriteLine($"发送消息{msg}");
                }

                //交换机类型3：Topic
                {
                    ////模糊匹配key，标识key有自己的规则，在direct的基础上可以模糊匹配key标识

                    //string exchangeTopic = "exchangeTopic";
                    //channel.ExchangeDeclare(
                    //    exchange: exchangeTopic,
                    //    type: ExchangeType.Topic,
                    //    durable: true,
                    //    autoDelete: false,
                    //    arguments: null
                    //    );

                    ////指定可以以routingKey.开头，后面可以写任意元素
                    //string routingKey_Topic1 = "routingKey.#";
                    //channel.QueueBind(
                    //    queue: queue1,
                    //    exchange: exchangeTopic,
                    //    routingKey: routingKey_Topic1,
                    //    arguments: null
                    //    );

                    ////指定可以以任意元素开头，结尾必须是.routing
                    //string routingKey_Topic2 = "#.routing";
                    //channel.QueueBind(
                    //    queue: queue2,
                    //    exchange: exchangeTopic,
                    //    routingKey: routingKey_Topic2,
                    //    arguments: null
                    //    );

                    //IBasicProperties basicProperties = channel.CreateBasicProperties();
                    ////指定持久化
                    ////basicProperties.Persistent = true;

                    //string msg = "模糊匹配key，交换机类型Topic测试";
                    //byte[] body = Encoding.UTF8.GetBytes(msg);
                    //channel.BasicPublish(
                    //    exchange: exchangeTopic,
                    //    //模糊匹配routingKey_Topic2，发送消息到队列2
                    //    routingKey: "sfdsfsd.routing",
                    //    basicProperties: basicProperties,
                    //    body: body
                    //    );

                    //Console.WriteLine($"发送消息{msg}");

                    //string msg1 = "模糊匹配key，交换机类型Topic测试";
                    //byte[] body1 = Encoding.UTF8.GetBytes(msg1);
                    //channel.BasicPublish(
                    //    exchange: exchangeTopic,
                    //    //模糊匹配routingKey_Topic1,发送消息到队列1
                    //    routingKey: "routingKey.ahhahahahah",
                    //    basicProperties: basicProperties,
                    //    body: body1
                    //    );

                    //Console.WriteLine($"发送消息{msg1}");
                }

                //交换机类型3：Headers
                {
                    //string exchangeHeaders = "exchangeHeaders";

                    //channel.ExchangeDeclare(
                    //    exchange: exchangeHeaders,
                    //    type: ExchangeType.Headers,
                    //    durable: true,
                    //    autoDelete: false,
                    //    arguments: null
                    //    );

                    //IDictionary<string, object> pairs = new Dictionary<string, object>
                    //{
                    //    //这个是写死的，必须要写这个，值可以使"all"，也可以是any
                    //    //如果是all，就是说发送消息的时候，参数列表里面需要和这里的参数全部一样
                    //    //如果是any，就是说发送消息的时候，参数列表里面只需要有一个和这里一样就行
                    //    {"x-match","all" },
                    //    {"name","张三" },
                    //    {"pwd","123456" }
                    //};
                    //channel.QueueBind(
                    //    queue: queue1,
                    //    exchange: exchangeHeaders,
                    //    routingKey: "",
                    //    arguments: pairs
                    //    );

                    //IDictionary<string, object> pairs1 = new Dictionary<string, object>
                    //{
                    //    //这个是写死的，必须要写这个，值可以使"all"，也可以是any
                    //    //如果是all，就是说发送消息的时候，参数列表里面需要和这里的参数全部一样
                    //    //如果是any，就是说发送消息的时候，参数列表里面只需要有一个和这里一样就行
                    //    {"x-match","any" },
                    //    {"name","张三" },
                    //    {"pwd","123456" }
                    //};
                    //channel.QueueBind(
                    //     queue: queue2,
                    //    exchange: exchangeHeaders,
                    //    routingKey: "",
                    //    arguments: pairs1
                    //    );

                    //IBasicProperties basicProperties = channel.CreateBasicProperties();
                    ////队列1和2都会发送消息，因为这里匹配name，pwd能成功，匹配name也能成功
                    //basicProperties.Headers = new Dictionary<string, object>
                    //{
                    //     {"name","张三" },
                    //    {"pwd","123456" }
                    //};

                    //string msg = "测试交换机类型Headers";
                    //byte[] body = Encoding.UTF8.GetBytes(msg);
                    //channel.BasicPublish(
                    //    exchange: exchangeHeaders,
                    //    routingKey: "",
                    //    basicProperties: basicProperties,
                    //    body: body
                    //    );

                    //Console.WriteLine($"发送消息：{msg}");


                    
                    //IBasicProperties basicProperties1 = channel.CreateBasicProperties();
                    ////只会给队列2发送消息，因为这里匹配name能成功，匹配不到pwd
                    //basicProperties1.Headers = new Dictionary<string, object>
                    //{
                    //     {"name","张三" }
                    //};

                    //string msg1 = "测试交换机类型Headers";
                    //byte[] body1 = Encoding.UTF8.GetBytes(msg1);
                    //channel.BasicPublish(
                    //    exchange: exchangeHeaders,
                    //    routingKey: "",
                    //    basicProperties: basicProperties1,
                    //    body: body1
                    //    );

                    //Console.WriteLine($"发送消息：{msg1}");


                }
            }
        }
    }
}
