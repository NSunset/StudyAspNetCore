using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Model
{
    /// <summary>
    /// 如果没有无参数构造函数，就需要打上特性DataContract。不然无法序列化，启动报错
    /// 属性打上DataMember特性，否则客户端不显示属性
    /// </summary>
    [DataContract]
    public class User
    {
        public User(int age)
        {

        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}
