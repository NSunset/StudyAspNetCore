using System;
using System.Reflection;

namespace Nw.MyAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            //特性属性Inherited 意义
            {

                //Type type = typeof(Userb);

                ////userb继承了usera。但是由于usera使用的特性MyTestAttribute指定了Inherited=false。无法继承使用
                ////所以这里userb没有标记特性MyTestAttribute
                //if (type.IsDefined(typeof(MyTestAttribute), true))
                //{
                //    foreach (var item in type.GetCustomAttributes(typeof(MyTestAttribute), true))
                //    {
                //        MyTestAttribute myTestAttribute = item as MyTestAttribute;


                //        Console.WriteLine($"{nameof(myTestAttribute.Id)}={myTestAttribute.Id}");
                //    }
                //}

                ////userb继承了usera。但是由于usera使用的特性MyTest1Attribute 默认指定了Inherited=true。可以继承使用
                ////所以这里userb有标记特性MyTest1Attribute
                //if (type.IsDefined(typeof(MyTest1Attribute), true))
                //{
                //    foreach (var item in type.GetCustomAttributes(typeof(MyTest1Attribute), true))
                //    {
                //        MyTest1Attribute myTestAttribute = item as MyTest1Attribute;


                //        Console.WriteLine($"{nameof(myTestAttribute.Id)}={myTestAttribute.Id}");
                //    }
                //}


            }


            //通过反射加特性实现实体属性验证(通过特性实现额外功能，验证)
            {
                //Student student = new Student
                //{
                //    Id = 123,
                //    Name = "张三"
                //};

                //ValidateResult result = student.Validate();

                //if (!result.IsOk)
                //{
                //    Console.WriteLine(result.ErrorMsg);
                //}
                //else
                //{
                //    Console.WriteLine("验证通过");
                //}
            }

            //通过反射加特性获取额外信息
            {
                Student student = new Student
                {
                    Id = 100,
                    Name = "田七",
                    Sex = Sex.Female
                };

                foreach (PropertyInfo item in student.GetType().GetProperties())
                {
                    if (item.IsDefined(typeof(RemarkAttribute), true))
                    {
                        RemarkAttribute remarkAttribute = item.GetCustomAttribute<RemarkAttribute>(true);
                        string remark = remarkAttribute.Remark;
                        Console.WriteLine($"{item.Name}的备注是{remark}");
                    }
                }

                foreach (FieldInfo item in student.GetType().GetFields())
                {
                    if (item.IsDefined(typeof(RemarkAttribute), true))
                    {
                        RemarkAttribute remarkAttribute = item.GetCustomAttribute<RemarkAttribute>(true);
                        string remark = remarkAttribute.Remark;
                        Console.WriteLine($"{item.Name}的备注是{remark}");
                    }
                }

                string sex = student.Sex.GetRemark();

                Console.WriteLine($"sdudent.Sex={sex}");
            }


            Console.ReadLine();
        }
    }
}
