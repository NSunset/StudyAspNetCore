using Nw.LiveBackgroundManage.HangfireService.Interface;
using Nw.LiveBackgroundManagement.Business.Interface;
using RedisHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManage.HangfireService.Service
{
    public class TimedTaskService : ITimedTaskService
    {
        private readonly ICSUserservice _ICSUserservice = null;
        private readonly ICSRoomService _ICSRoomService = null;
        private readonly RedisStringService _RedisStringService;

        public TimedTaskService(
            ICSUserservice iCSUserservice,
            ICSRoomService iCSRoomService,
            RedisStringService redisStringService)
        {
            this._ICSUserservice = iCSUserservice;
            this._ICSRoomService = iCSRoomService;
            this._RedisStringService = redisStringService;
        }

        public void SynchronizeFieldData()
        {
            _ICSUserservice.SynchronizeFieldData();
        }
    }
}
