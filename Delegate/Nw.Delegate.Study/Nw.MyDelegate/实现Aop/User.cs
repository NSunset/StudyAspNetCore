using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyDelegate
{
    public class User
    {

        /// <summary>
        /// 显示用户信息
        /// 使用AOP在不修改内部代码的情况下添加新的功能:执行方法前输出请求参数，执行方法后输出打印日志
        /// 通过委托实现
        /// </summary>
        [Input]
        [WriteLog]
        public void Show()
        {
            Console.WriteLine("显示用户信息");
        }

    }
}
