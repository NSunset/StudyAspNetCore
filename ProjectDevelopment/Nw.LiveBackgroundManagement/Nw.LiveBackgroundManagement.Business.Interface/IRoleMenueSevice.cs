using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Models.ViewModel;

namespace Nw.LiveBackgroundManagement.Business.Interface
{

    /// <summary>
    /// 
    /// </summary>
    public interface IRoleMenueSevice : IBaseService
    {
        public List<MenueViewModel> FindCurrentUserMenue(int userId);
    }

}
