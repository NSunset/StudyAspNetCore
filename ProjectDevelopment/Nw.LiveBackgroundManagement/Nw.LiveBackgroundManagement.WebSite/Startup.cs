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
            //���������������
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            services.AddControllersWithViews()
                //֧���޸�Դ�����������Ч
                .AddRazorRuntimeCompilation();

            //�����ļ�����
            services.AddConfig(Configuration);

            //����AutoMapper��ʵ��ת��
            services.AddAutoMapper(typeof(ServiceProfile));

            //���ü�Ȩ��Ȩ
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login"); //�����Ȩʧ�ܣ�����ת�����·��ȥ��
                    options.AccessDeniedPath = new PathString("/Account/Error");
                });

            //���ü�Ȩ����
            services.AddAuthorization(options =>
            {
                //��Ӳ���
                options.AddPolicy(CustomAuthorizationHandler.MenuPolicy, builder =>
                {
                    //��Ӳ�����֤��Ҫ�Ĳ���
                    builder.AddRequirements(new CustomAuthorizationRequirement(PolicyEnum.MenuPermissions));
                });
            });
            //ע�������֤��ʵ����
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

            //ʹ��Websocket
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
