using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Behavior
{
    public class CEO : BaseAudit
    {
        public override void Audit(ApplyContext applyContext)
        {
            if (applyContext.Hour<72)
            {
                applyContext.AuditResult = true;
                applyContext.AuditRemark = $"{Name}审核通过";
            }
            else
            {
                NextAudit(applyContext);
            }
        }
    }
}
