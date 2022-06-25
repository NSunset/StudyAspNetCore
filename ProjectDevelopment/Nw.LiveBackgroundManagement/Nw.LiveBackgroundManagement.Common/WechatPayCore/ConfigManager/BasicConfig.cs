using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common.WechatPayCore.ConfigManager
{
    public class BasicConfig
    {
        public const string WxPayConfigure = "WechatConfig";
        public string AppID { get; set; }
        public string MchID { get; set; }
        public string Key { get; set; }
    }
}
