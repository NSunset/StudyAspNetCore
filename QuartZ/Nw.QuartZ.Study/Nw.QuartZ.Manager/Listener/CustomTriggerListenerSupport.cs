using Quartz;
using Quartz.Listener;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nw.QuartZ.Manager
{
    public class CustomTriggerListenerSupport : TriggerListenerSupport
    {
        public override string Name => "CustomTriggerListener";

        /// <summary>
        /// 触发器完成时执行
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="triggerInstructionCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("触发器完成时执行");
            return base.TriggerComplete(trigger, context, triggerInstructionCode, cancellationToken);
        }

        /// <summary>
        /// 触发器触发时执行
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("触发器出发时执行");
            return base.TriggerFired(trigger, context, cancellationToken);
        }

        /// <summary>
        /// 触发器失效时执行
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("触发器失效");
            return base.TriggerMisfired(trigger, cancellationToken);
        }

        /// <summary>
        /// 不执行作业时执行
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("触发器事件不执行作业");
            return base.VetoJobExecution(trigger, context, cancellationToken);
        }
    }
}
