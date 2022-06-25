using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Behavior
{
    /// <summary>
    /// 行为型设计模式测试入口
    /// </summary>
    public class BehaviorEntrance
    {
        public static void Show()
        {
            //观察者模式
            {
                //Cat cat = new Cat();
                //Dog dog = new Dog();
                //Mouse mouse = new Mouse();
                //cat.SubscribeEventHandler += dog.WokeUp;
                //cat.SubscribeEventHandler += mouse.Run;
                //cat.Miao();
            }

            //责任链模式
            {
                //ApplyContext applyContext = new ApplyContext
                //{
                //    Id = 10,
                //    Name = "张三",
                //    Hour = 18,
                //    Description = "病假"
                //};

                ////pm审核24小时，Charge审核48小时,
                //BaseAudit pm = new Pm
                //{
                //    Name = "李四"
                //};
                ////Charge charge = new Charge
                ////{
                ////    Name = "王五"
                ////};
                //CEO cEO = new CEO
                //{
                //    Name = "赵六"
                //};
                ////pm.SetNextAudit(charge);

                ////charge.SetNextAudit(cEO);

                //pm.SetNextAudit(cEO);
                //pm.Audit(applyContext);
                //Console.WriteLine(applyContext.AuditRemark);
            }



        }
    }
}
