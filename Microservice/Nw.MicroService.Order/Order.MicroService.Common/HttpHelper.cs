using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Order.MicroService.Common
{
    /// <summary>
    /// http连接基础类，负责底层的http通信
    /// </summary>
    public class HttpHelper
    {
        public static T HttpRequest<T>(string url, HttpMethod httpMethod, Dictionary<string, string> parameter)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage()
                {
                    Method = httpMethod,
                    RequestUri = new Uri(url)
                };
                if (parameter != null)
                {
                    var encodedContent = new FormUrlEncodedContent(parameter);
                    message.Content = encodedContent;
                }
                var result = httpClient.SendAsync(message).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string sResult = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(sResult);
                }
                else
                {
                    Console.WriteLine("发生异常了");
                    throw new Exception($"{url}调用失败");
                }
            }
        }
    }
}
