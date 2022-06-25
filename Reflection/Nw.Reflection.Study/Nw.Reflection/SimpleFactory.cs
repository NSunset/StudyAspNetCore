using Nw.Db.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nw.Reflection
{
    public class SimpleFactory
    {
        /// <summary>
        /// 创建DbHelp实例
        /// 通过修改配置文件DbHelpConfig节点，修改使用的数据库
        /// </summary>
        /// <returns></returns>
        public static IDbHelp CreateDbHelp()
        {
            ////通过类库名称加dll后缀获取到类库清单数据(必须把类库放到执行文件目录下)
            //Assembly assembly = Assembly.LoadFrom("Nw.Db.Mysql.dll");

            ////通过具体的类名称（包括命名空间的全名称）获取具体类型
            //Type mysqlType = assembly.GetType("Nw.Db.Mysql.MySqlHelp");


            string config = CustomConfigManager.GetConfig("DbHelpConfig");

            string[] dbHelpConfig = config.Split(",");

            //通过类库名称加dll后缀获取到类库清单数据(必须把类库放到执行文件目录下)
            Assembly assembly = Assembly.LoadFrom(dbHelpConfig[0]);

            //通过具体的类名称（包括命名空间的全名称）获取具体类型
            Type mysqlType = assembly.GetType(dbHelpConfig[1]);

            //通过Activator创建mysqlHelp类型的实例
            object obj = Activator.CreateInstance(mysqlType);

            //强转为接口
            IDbHelp dbHelp = obj as IDbHelp;

            return dbHelp;
        }
    }
}
