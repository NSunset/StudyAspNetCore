using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyEncryption
{
    public static class Constant
    {
        public static string DesKey = AppSettings("DesKey","");

        private static T AppSettings<T>(string key,T defaultValue)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("AppSettings.json");

            IConfigurationRoot builder = configurationBuilder.Build();
            string configResult = builder.GetSection(key).Value;

            T result = string.IsNullOrWhiteSpace(configResult) ? defaultValue : (T)Convert.ChangeType(configResult, typeof(T));

            return result;

        }
    }
}
