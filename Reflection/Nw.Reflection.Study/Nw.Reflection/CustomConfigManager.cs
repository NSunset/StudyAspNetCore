using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Nw.Reflection
{
    public class CustomConfigManager
    {
        /// <summary>
        /// 根据配置文件Key获取配置文件Key对应的Value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            IConfigurationBuilder configurationBuilder= new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot build = configurationBuilder.Build();
            return build.GetSection(key).Value;
        }
    }
}
