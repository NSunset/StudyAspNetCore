using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Common;

namespace Nw.LiveBackgroundManagement.Service.Test
{
    public class TestOptions<T> : IOptions<T> where T : class
    {
        public T Value => GetConfigOptions();

        public T GetConfigOptions()
        {
            if (typeof(T)==typeof(ConnectionStringsConfigure))
            {
                var options=new ConnectionStringsConfigure
                {
                    AuthorityDbContext = "server=192.168.157.128;database=LiveBackgroundManagement;uid=root;pwd=root;charset=utf8"
                };

                return options as T;
            }
            if (typeof(T) == typeof(RedisConfigure))
            {
                var options = new RedisConfigure
                {
                    WriteServerList=new List<string> { "192.168.157.128:6379" }
                };

                return options as T;
            }
            return null;

        }
    }
}
