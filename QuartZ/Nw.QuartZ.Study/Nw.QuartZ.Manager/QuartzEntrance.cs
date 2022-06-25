using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nw.QuartZ.Manager
{
    public class QuartzEntrance
    {
        public async static void Init()
        {
            //使用Quartz
            //1、安装Quartz包
            //2、创建StdSchedulerFactory
            //3、创建IScheduler
            //4、创建执行定时任务的类TestJob
            //5、创建IJobDetail,指定要执行的任务是TestJob
            //6、创建ITrigger
            StdSchedulerFactory factory = new Quartz.Impl.StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<TestJob>()
                .Build();

            //指定时间策略
            ITrigger trigger = TriggerBuilder.Create()
            .WithSimpleSchedule(builder =>
            {
                builder.WithIntervalInSeconds(3)//间隔3秒执行一次
                .RepeatForever()//永远重复
                ;
            })
            //每周1-5下午16:51分执行一次
            //.WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(16, 51, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday))
            //.WithCronSchedule("0 51 16 ? * MON-FRI")
            .Build();


            //需要IJobDetail和ITrigger
            await scheduler.ScheduleJob(jobDetail, trigger);

            //添加job执行切面事件（执行前，执行后，未执行）
            //scheduler.ListenerManager.AddJobListener(new CustomJobListenerSupport());
            //scheduler.ListenerManager.AddTriggerListener(new CustomTriggerListenerSupport());



        }
    }
}
