using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    public class ManicureDecorate : BaseMakeUpDecorate
    {
        public ManicureDecorate(AbstractGodDess baseMakeUp) : base(baseMakeUp)
        {
        }

        public override void Fig()
        {
            base.Fig();
            Console.WriteLine($"美甲");
        }
    }
}
