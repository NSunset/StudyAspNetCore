using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            this.Remark = remark;
        }

        public string Remark { get; }
    }
}
