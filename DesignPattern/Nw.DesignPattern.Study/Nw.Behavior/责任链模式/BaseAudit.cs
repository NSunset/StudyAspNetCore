using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Behavior
{
    public abstract class BaseAudit
    {
        private BaseAudit _nextAudit;

        public string Name { get; set; }

        public abstract void Audit(ApplyContext applyContext);

        public void SetNextAudit(BaseAudit baseAudit)
        {
            _nextAudit = baseAudit;
        }

        protected void NextAudit(ApplyContext applyContext)
        {
            if (_nextAudit != null)
            {
                _nextAudit.Audit(applyContext);
            }
            else
            {
                applyContext.AuditRemark = "审核不通过";
                applyContext.AuditResult = false;
            }
        }

    }
}
