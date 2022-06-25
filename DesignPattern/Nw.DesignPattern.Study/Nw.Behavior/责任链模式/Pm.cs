using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Behavior
{
    public class Pm : BaseAudit
    {
        public override void Audit(ApplyContext applyContext)
        {
            if (applyContext.Hour < 24)
            {
                applyContext.AuditResult = true;
                applyContext.AuditRemark = $"PM{Name}审核通过";
            }
            else
            {
                NextAudit(applyContext);
            }
        }
    }
}
