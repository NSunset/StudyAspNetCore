using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    public abstract class BaseMakeUpDecorate : AbstractGodDess
    {
        //通过组合加继承的方式实现装饰器
        protected AbstractGodDess _baseMakeUp;
        public BaseMakeUpDecorate(AbstractGodDess baseMakeUp)
        {
            _baseMakeUp = baseMakeUp;
        }

        public override void Fig()
        {
            _baseMakeUp.Fig();
        }
    }
}
