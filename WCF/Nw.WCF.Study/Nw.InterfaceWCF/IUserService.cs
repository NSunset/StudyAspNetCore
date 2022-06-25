using Nw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Nw.InterfaceWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserService”。
    /// <summary>
    /// 这里服务支持回调
    /// </summary>
    //[ServiceContract]
    [ServiceContract(CallbackContract = typeof(ICallBackService))]
    public interface IUserService
    {
        //[OperationContract]
        //void DoWork();

        /// <summary>
        /// 添加用户
        /// 不标记特性，客户端无法找到这个方法
        /// </summary>
        /// <param name="user"></param>
        [OperationContract]
        void AddUser(User user);

        /// <summary>
        /// 查找用户
        /// 不标记特性，客户端无法找到这个方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        User Find(int id);
    }
}
