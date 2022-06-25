using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    public static class ResultMsgExpand
    {
        public static byte[] GetByte(this ResultMsg resultMsg)
        {
            string text = JsonConvert.SerializeObject(resultMsg);
            byte[] msg = Encoding.UTF8.GetBytes(text);
            return msg;
        }

        public static string GetString(this ResultMsg resultMsg)
        {
            string text = JsonConvert.SerializeObject(resultMsg);
            return text;
        }
    }
}
