using Nw.Db.Interface;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Nw.Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            try
            {
                //通过反射获取数据库访问帮助类
                {
                    ////通过类库名称加dll后缀获取到类库清单数据(必须把类库放到执行文件目录下)
                    //Assembly assembly = Assembly.LoadFrom("Nw.Db.Mysql.dll");

                    ////通过具体的类名称（包括命名空间的全名称）获取具体类型
                    //Type mysqlType = assembly.GetType("Nw.Db.Mysql.MySqlHelp");

                    ////通过Activator创建mysqlHelp类型的实例
                    //object obj = Activator.CreateInstance(mysqlType);

                    ////强转为接口
                    //IDbHelp dbHelp = obj as IDbHelp;

                    ////调用数据访问帮助类获取数据
                    //dbHelp.Get();

                    //通过简单工厂设计模式，把复杂的创建DbHelp工作封装到工厂类中
                    //IDbHelp dbHelp = SimpleFactory.CreateDbHelp();
                    //dbHelp.Get();
                }

                //反射可以访问对象私有元素。破坏权限控制
                {
                    //单例无法直接new
                    //Singleton singleton = new Singleton();

                    //Singleton singleton = Singleton.GetSingleton();


                    ////通过反射获取试试
                    //Type type = typeof(Singleton);

                    //object obj = Activator.CreateInstance(type,true);

                    //Singleton singleton1 = obj as Singleton;

                }


                //通过反射创建ReflectionTest对象实例。
                //通过制定参数的个数类型不同，调用不同的构造函数
                {
                    //string dllName = "Nw.Db.Mysql.dll";
                    //string className = "Nw.Db.Mysql.ReflectionTest";


                    //Assembly dll = Assembly.LoadFrom(dllName);

                    //Type classType = dll.GetType(className);

                    //object reflectionTest = Activator.CreateInstance(classType);

                    //object reflectionTest1 = Activator.CreateInstance(classType,new object[] {10 });

                    //object reflectionTest2 = Activator.CreateInstance(classType, new object[] { "张三" });

                    //object reflectionTest3 = Activator.CreateInstance(classType, new object[] { 20,"李四" });
                }

                //通过反射调用ReflectionTest对象的方法
                {
                    //string dllName = "Nw.Db.Mysql.dll";
                    //string className = "Nw.Db.Mysql.ReflectionTest";

                    //Assembly dll = Assembly.LoadFrom(dllName);

                    //Type classType = dll.GetType(className);

                    //object reflectionTest = Activator.CreateInstance(classType);

                    ////无参数重载方法Show调用
                    //{
                    //    MethodInfo show = classType.GetMethod("Show", Type.EmptyTypes);

                    //    show.Invoke(reflectionTest, null);
                    //}

                    ////无参数方法Show1调用
                    //{
                    //    MethodInfo show = classType.GetMethod("Show1");
                    //    show.Invoke(reflectionTest, null);
                    //}

                    ////私有方法Show2调用,参数string
                    //{
                    //    MethodInfo show = classType.GetMethod("Show2", BindingFlags.NonPublic | BindingFlags.Instance);

                    //    show.Invoke(reflectionTest, new object[] { "张三" });
                    //}

                    ////私有重载方法Show4调用
                    //{
                    //    MethodInfo show4_1 = classType.GetMethod("Show4", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(string) }, null);

                    //    show4_1.Invoke(reflectionTest, new object[] { "朱八" });

                    //    MethodInfo show4_2 = classType.GetMethod("Show4", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(int) }, null);

                    //    show4_2.Invoke(reflectionTest, new object[] { 88 });
                    //}

                    ////静态方法Show3调用
                    //{
                    //    MethodInfo show = classType.GetMethod("Show3", BindingFlags.Static | BindingFlags.Public);

                    //    show.Invoke(reflectionTest, new object[] { "李四" });
                    //}

                    ////静态重载方法Show5调用
                    //{
                    //    MethodInfo show5_1 = classType.GetMethod("Show5", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string) }, null);

                    //    show5_1.Invoke(reflectionTest, new object[] { "龙九" });

                    //    MethodInfo show5_2 = classType.GetMethod("Show5", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(int) }, null);

                    //    show5_2.Invoke(reflectionTest, new object[] { 99 });
                    //}

                    ////静态私有方法Show6调用
                    //{
                    //    MethodInfo show = classType.GetMethod("Show6", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);

                    //    show.Invoke(reflectionTest, null);


                    //}
                    ////静态私有重载方法Show6调用
                    //{
                    //    MethodInfo show = classType.GetMethod("Show6", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(int) }, null);

                    //    show.Invoke(reflectionTest, new object[] { 66 });


                    //}

                    ////重载方法调用
                    //{
                    //    MethodInfo show_1 = classType.GetMethod("Show", new Type[] { typeof(int) });

                    //    show_1.Invoke(reflectionTest, new object[] { 10 });


                    //    MethodInfo show_2 = classType.GetMethod("Show", new Type[] { typeof(string) });

                    //    show_2.Invoke(reflectionTest, new object[] { "王五" });

                    //    MethodInfo show_3 = classType.GetMethod("Show", new Type[] { typeof(int), typeof(string) });

                    //    show_3.Invoke(reflectionTest, new object[] { 55, "赵六" });

                    //    MethodInfo show_4 = classType.GetMethod("Show", new Type[] { typeof(string), typeof(int) });

                    //    show_4.Invoke(reflectionTest, new object[] { "田七", 99 });
                    //}

                }

                //通过反射调用泛型对象
                {
                    //string dllName = "Nw.Db.Mysql.dll";

                    //Assembly dll = Assembly.LoadFrom(dllName);
                    ////调用泛型方法
                    //{
                    //    string className = "Nw.Db.Mysql.GenericMethod";

                    //    Type genericMethodType = dll.GetType(className);

                    //    object instance = Activator.CreateInstance(genericMethodType);

                    //    MethodInfo show = genericMethodType.GetMethod("Show").MakeGenericMethod(new Type[] { typeof(int), typeof(string), typeof(DateTime) });

                    //    show.Invoke(instance, new object[] { 10, "张三", DateTime.Now });
                    //}

                    ////调用泛型类,内部方法泛型参数依赖类
                    //{
                    //    string className = "Nw.Db.Mysql.GenericClass`3";

                    //    Type genericClassType= dll.GetType(className).MakeGenericType(new Type[] { typeof(int), typeof(string), typeof(DateTime) });

                    //    object instance = Activator.CreateInstance(genericClassType);

                    //    MethodInfo show = genericClassType.GetMethod("Show");

                    //    show.Invoke(instance, new object[] { 11, "李四", DateTime.Now });
                    //}

                    ////调用泛型类，内部方法有依赖类，又有自己的泛型
                    //{
                    //    string className = "Nw.Db.Mysql.GenericDouble`1";

                    //    Type genericDoubleType = dll.GetType(className).MakeGenericType(new Type[] { typeof(int) });

                    //    object instance = Activator.CreateInstance(genericDoubleType);

                    //    MethodInfo show = genericDoubleType.GetMethod("Show").MakeGenericMethod(new Type[] { typeof(string), typeof(DateTime) });

                    //    show.Invoke(instance, new object[] { 12, "王五", DateTime.Now });
                    //}

                }

                //通过反射获取对象字段和属性。
                {
                    User user = new User(123, "张三", DateTime.Now);

                    Type type = typeof(User);

                   
                    //属性
                    foreach (PropertyInfo item in type.GetProperties())
                    {
                        Console.WriteLine($"{item.Name}={item.GetValue(user)}");
                    }

                    //字段操作
                    foreach (FieldInfo item in type.GetFields())
                    {
                        Console.WriteLine($"{item.Name}={item.GetValue(user)}");
                    }

                    Dictionary<string, object> keyValues = new Dictionary<string, object>()
                    {
                        {"Id",987 },
                        {"Name","李四" },
                         {"Time",DateTime.Now.AddDays(1) },
                          {"_sex","女" },
                          {"_message","坏消息" },
                    };

                    object result = Activator.CreateInstance(type);

                    foreach (PropertyInfo item in type.GetProperties())
                    {
                        item.SetValue(result, keyValues[item.Name]);
                    }

                    foreach (PropertyInfo item in result.GetType().GetProperties())
                    {
                        Console.WriteLine($"{item.Name}={item.GetValue(result)}");
                    }

                    //字段操作
                    foreach (FieldInfo item in type.GetFields())
                    {
                        item.SetValue(result, keyValues[item.Name]);
                    }
                    foreach (FieldInfo item in result.GetType().GetFields())
                    {
                        Console.WriteLine($"{item.Name}={item.GetValue(result)}");
                    }

                    Console.WriteLine();
                }


                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
