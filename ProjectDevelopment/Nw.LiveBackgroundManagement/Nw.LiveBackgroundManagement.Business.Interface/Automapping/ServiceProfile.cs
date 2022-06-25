using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.ViewModel;

namespace Nw.LiveBackgroundManagement.Business.Interface.Automapping
{
    public class ServiceProfile: Profile
    {
        public ServiceProfile()
        {
            //CreateMap<SysMenu, MenueViewModel>().ReverseMap();
            CreateMap<RoomTypeViewModel, CSRoomType>().ReverseMap();
            //CreateMap<RoomViewModel, CSRoom>().ReverseMap();
            CreateMap<CSUser, UserViewModel>().ReverseMap();
            CreateMap<CSUserApply, CSUserApplyViewModel>().ReverseMap();

            CreateMap<CSUser, CSUserViewModel>().ReverseMap();

            CreateMap<CSComment, CSCommentViewModel>().ReverseMap();

            CreateMap<CSWorks, CSWorkViewModel>().ReverseMap();

            CreateMap<CSRoom, CSRoomModel>().ReverseMap();
            CreateMap<CSIntegralRechargeDetailViewModel, CSIntegralRechargeDetail>().ReverseMap();

            CreateMap<CSIntegralRecharge, CSIntegralRechargeViewModel>().ReverseMap();
        }
    }
}
