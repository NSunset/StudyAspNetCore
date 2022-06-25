using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using Nw.LiveBackgroundManagement.Business.Interface.Automapping;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.WebSite.Utility.AuthorizationPolicy;
using Nw.LiveBackgroundManagement.WebSite.Utility.CustomWebSocket;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Nw.LiveBackgroundManagement.WebSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //解决中文乱码问题
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            services.AddControllersWithViews()
                //支持修改源码后马上升生效
                .AddRazorRuntimeCompilation();

            //配置文件配置
            services.AddConfig(Configuration);

            //配置AutoMapper，实体转化
            services.AddAutoMapper(typeof(ServiceProfile));

            //配置鉴权授权
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login"); //如果授权失败，就跳转到这个路径去中
                    options.AccessDeniedPath = new PathString("/Account/Error");
                });

            //配置鉴权策略
            services.AddAuthorization(options =>
            {
                //添加策略
                options.AddPolicy(CustomAuthorizationHandler.MenuPolicy, builder =>
                {
                    //添加策略验证需要的参数
                    builder.AddRequirements(new CustomAuthorizationRequirement(PolicyEnum.MenuPermissions));
                });
            });
            //注册策略验证的实现类
            services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AotoFacConfig.AutofacModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //使用Websocket
            app.Map("/WebSocketConnect", WebSocketConnect.MapWebSocket);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
