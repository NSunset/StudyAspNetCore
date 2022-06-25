using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Nw.LiveBackgroundManagement.DataAccessEFCore;

namespace Nw.LiveBackgroundManagement.Business.Services
{
    public class RoleMenueSevice : BaseService, IRoleMenueSevice
    {
        public RoleMenueSevice(AuthorityDbContext context) : base(context)
        {

        }

        public List<MenueViewModel> FindCurrentUserMenue(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
