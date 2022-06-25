using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nw.Client
{
    public class Input
    {
        public int Key { get; set; }

        public object Body { get; set; }

        public byte[] GetByte()
        {
            string text = JsonConvert.SerializeObject(Body);
            List<byte> bytes = new List<byte>();
            byte[] head = BitConverter.GetBytes(Key);

            byte[] body = Encoding.UTF8.GetBytes(text);
            byte[] bodyLeng = BitConverter.GetBytes(body.Length);

            bytes.AddRange(head);
            bytes.AddRange(bodyLeng);
            bytes.AddRange(body);

            return bytes.ToArray();
        }
    }
}
