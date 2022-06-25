using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Business.Interface.AopExtension;
using Nw.LiveBackgroundManagement.Business.Services;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using RedisHelper.Interface;
using RedisHelper.Service;

namespace Nw.LiveBackgroundManagement.WebSite.AotoFacConfig
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorityDbContext>().As<DbContext>();
            builder.RegisterType<CSRoomService>().As<ICSRoomService>();
            builder.RegisterType<CSUserservice>().As<ICSUserservice>();

            //缓存排名
            builder.RegisterType<CustomAutofacStatisticsAop>();
            builder.RegisterType<CSStatisticsService>().As<ICSStatisticsService>()
                //启用aop
                .EnableClassInterceptors()
                ;

            builder.RegisterType<RoleMenueSevice>().As<IRoleMenueSevice>();

            builder.RegisterType<CustomAutofacCacheAop>();
            builder.RegisterType<SysUserservice>().As<ISysUserservice>().
                //配置aop
                EnableClassInterceptors();

            #region redis配置

            builder.RegisterType<RedisConfigureHelper>().As<IRedisConfigureHelper>();
            builder.RegisterType<RedisHashService>();
            builder.RegisterType<RedisListService>();
            builder.RegisterType<RedisStringService>();
            builder.RegisterType<RedisZSetService>();

            #endregion

            //builder.RegisterType(typeof(CustomAutofacLogAop));
            //base.Load(builder);
        }
    }
}
