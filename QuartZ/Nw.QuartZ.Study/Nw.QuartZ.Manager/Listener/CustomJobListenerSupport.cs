using Quartz;
using Quartz.Listener;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nw.QuartZ.Manager
{
    public class CustomJobListenerSupport : JobListenerSupport
    {
        public override string Name => "CustomJobListener";

        /// <summary>
        /// JOB不执行了，执行这里
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("JOB没有执行");
            return base.JobExecutionVetoed(context, cancellationToken);
        }

        /// <summary>
        /// JOB执行前执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("JOB之前执行");
            return base.JobToBeExecuted(context, cancellationToken);
        }

        /// <summary>
        /// JOB执行完后执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("JOB之后执行");
            return base.JobWasExecuted(context, jobException, cancellationToken);
        }
    }
}
