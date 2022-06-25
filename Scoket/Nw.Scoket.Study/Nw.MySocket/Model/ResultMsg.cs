using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    public class ResultMsg
    {
        public bool IsOk { get; set; }

        public KeyType Type { get; set; }

        public object Msg { get; set; }

        public static ResultMsg Error(KeyType keyType, string errorMsg)
        {
            return new ResultMsg
            {
                IsOk = false,
                Type = keyType,
                Msg = errorMsg
            };
        }

        public static ResultMsg Ok<T>(KeyType keyType, T t)
        {
            return new ResultMsg
            {
                IsOk = true,
                Type = keyType,
                Msg = t
            };
        }

    }
}
