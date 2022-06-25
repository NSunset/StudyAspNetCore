using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    /// <summary>
    /// 结构型设计模式测试入口
    /// </summary>
    public class Entrance
    {
        public static void Show()
        {
            //适配器模式：让不能一起使用的类可以一起使用
            {
                //IDbHelp dbHelp = new MySqlHelp();
                //dbHelp.Add<Entrance>(new Entrance());

                //IDbHelp dbHelp1 = new SqlServerHelp();
                //dbHelp1.Add(new Entrance());

                //为了解决这两个类无法一起使用。提出的方法
                //IDbHelp dbHelp2 = new RedisHelp();
                //IDbHelp dbHelp = new RedisAdapter(new RedisHelp());
                //dbHelp.Add(new Entrance()); 

            }

            //代理模式：创建一个代理对象，拥有原有对象的一切引用。可以扩展公共逻辑，不能扩展业务逻辑
            {
                //ISubject subject = new Student();
                //subject.Add();
                //为了实现在Student做操作时添加公共逻辑，提出的方法。
                //ISubject subject = new StudentProxy();
                //subject.Add();
            }


            //装饰器模式：在不修改源代码的情况下添加新的功能，并为这些功能排序
            {
                //想要新的功能组合，并为他们排序.出来了装饰器

                AbstractGodDess godDess = new GodDess()
                {
                    Name = "美女"
                };
                //画眼影
                godDess = new EyeShadowDecorate(godDess);
                //美甲
                godDess = new ManicureDecorate(godDess);

                godDess.Fig();
            }
        }
    }
}
