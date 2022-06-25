using Nw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Nw.InterfaceWCF
{
    public interface ICallBackService
    {
        /// <summary>
        /// 表示支持回调
        /// </summary>
        /// <param name="user"></param>
        [OperationContract(IsOneWay = true)]
        void CallBack(User user);
    }
}
