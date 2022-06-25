using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    public class EyeShadowDecorate : BaseMakeUpDecorate
    {
        public EyeShadowDecorate(AbstractGodDess baseMakeUp) : base(baseMakeUp)
        {
        }

        public override void Fig()
        {
            base.Fig();
            Console.WriteLine($"画眼影");
        }
    }
}
