using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    /// <summary>
    /// 女性
    /// </summary>
    public class GodDess : AbstractGodDess
    {
        public string Name { get; set; }


        public override void Fig()
        {
            Console.WriteLine($"{Name}洗脸");
        }
    }

    public abstract class AbstractGodDess
    {
        public virtual void Fig()
        {
            Console.WriteLine($"洗脸");
        }
    }
}
