using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Quartz.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //这里库版本不同，无法使用。linux可以使用docker管理，不需要windows服务
            //QuartzEntrance.Init();
            //使用C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe 当前项目启动路径
            //安装服务

            //使用C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u 当前项目启动路径
            //卸载服务
        }

        protected override void OnStop()
        {
        }
    }
}
