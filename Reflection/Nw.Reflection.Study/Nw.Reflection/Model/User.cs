using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Reflection
{
    public class User
    {

        public string _sex = "男";

        public string _message = "好消息";

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public User()
        {

        }

        public User(int id,string name,DateTime time)
        {
            this.Id = id;
            this.Name = name;
            this.Time = time;
        }
    }
}
