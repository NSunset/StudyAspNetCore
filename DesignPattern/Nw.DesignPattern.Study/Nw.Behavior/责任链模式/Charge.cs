using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Behavior
{
    public class Charge : BaseAudit
    {
        public override void Audit(ApplyContext applyContext)
        {
            if (applyContext.Hour < 48)
            {
                applyContext.AuditResult = true;
                applyContext.AuditRemark = $"经理{Name}审核通过";
            }
            else
            {
                NextAudit(applyContext);
            }
        }
    }
}
