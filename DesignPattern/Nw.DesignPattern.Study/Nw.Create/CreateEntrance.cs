using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Create
{
    public class CreateEntrance
    {
        public static void Show()
        {
            //创建型设计模式
            {
                //User u = new User();

                //for (int i = 0; i < 1000000; i++)
                //{
                //    Task.Run(() =>
                //    {
                //        //通过双判断加锁实现单例，反多线程,懒汉式
                //        {
                //            //User u = User.CreateExample();
                //        }

                //        //通过静态构造函数或者静态字段实现单例，多线程不影响，饿汉式
                //        {
                //            //User u = User.Show();
                //        }

                //        //通过泛型来扩展单例的实现,让其他类直接使用
                //        {
                //            //UserChild u = Singleton<UserChild>.CreateInstance();
                //        }
                //    });
                //}

            }

            //原型模式
            {
                //快速创建重复对象,基于内存拷贝
                //Student student = Student.CreateInstance();
                //student.Id = 10;
                //student.Name = "张三";

                //Student student1 = Student.CreateInstance();
                //student1.Id = 20;
                //student1.Name = "李四";

                //Student student2 = Student.GetStudentClone();
                //student2.Id = 20;
                //student2.Name = "李四";

                //Console.WriteLine($"id={student.Id};name={student.Name}");
            }
        }
    }
}
