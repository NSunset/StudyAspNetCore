using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket.ReceiveFilter
{
    public class CustomReceiveFilter : FixedHeaderReceiveFilter<CustomRequestInfo>
    {
        public CustomReceiveFilter() : base(8)
        {

        }

        

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            //return (int)header[offset + 4] * 256 + (int)header[offset + 5];
            return GetBodyLengthFromHeader(header, offset, length, 4, 4);//4表示第几个字节开始表示长度.4:由于是int来表示长度,int占用4个字节
        }

        protected override CustomRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            int key = BitConverter.ToInt32(header.ToArray(), 0);
            KeyType keyType = (KeyType)key;

            string text = Encoding.UTF8.GetString(bodyBuffer.CloneRange(offset, length));
            return new CustomRequestInfo
            {
                Key = keyType.ToString(),//这是命令名称，Commands里面的类名如果想和这个一样，那么必须重写Name属性，让Name属性和这个Key一样
                Body = text
            };
        }

        private int GetBodyLengthFromHeader(byte[] header, int offset, int length, int lenStartIndex, int lenBytesCount)
        {
            var headerData = new byte[lenBytesCount];
            Array.Copy(header, offset + lenStartIndex, headerData, 0, lenBytesCount);//
            int i = BitConverter.ToInt32(headerData, 0);
            return i;
        }
    }
}
