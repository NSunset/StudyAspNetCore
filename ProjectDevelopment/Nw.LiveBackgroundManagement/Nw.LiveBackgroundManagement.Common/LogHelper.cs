using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common
{
    public class LogHelper
    {
        private static ILog Log;
        private static ILoggerRepository loggerRepository { get; set; }
        static LogHelper()
        {
            loggerRepository = LogManager.CreateRepository("Log4netConsolePractice");

            var file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log4net.config"));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, file);
            Log = LogManager.GetLogger("Log4netConsolePractice", "loginfo");
        }

        public static void Debug(string write)
        {
            Log.Debug("日志记录:" + write);
        }

        public static void Debug(string write, Exception ex)
        {
            Log.Debug("日志记录:" + write + "。错误记载：" + ex.ToString());
        }


        public static void Info(string write)
        {
            Log.Info("日志记录:" + write);
        }

        public static void Info(string write, Exception ex)
        {
            Log.Info("日志记录:" + write + "。错误记载：" + ex.ToString());
        }

        public static void Error(string write)
        {
            Log.Error("日志记录:" + write);
        }

        public static void Error(string write, Exception ex)
        {
            Log.Error("日志记录:" + write + "。错误记载：" + ex.ToString());
        }
    }
}
