using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Behavior
{
    /// <summary>
    /// 请假单上下文
    /// </summary>
    public class ApplyContext
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Hour { get; set; }

        public string Description { get; set; }

        public bool AuditResult { get; set; }

        public string AuditRemark { get; set; }
    }
}
