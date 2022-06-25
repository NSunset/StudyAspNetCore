using Autofac;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using Microsoft.OpenApi.Models;
using Nw.LiveBackgroundManage.HangfireService;
using Nw.LiveBackgroundManage.WebApi.AotoFacConfig;
using Nw.LiveBackgroundManagement.Business.Interface.Automapping;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using Nw.LiveReceptionManage.WebApi.EventHandler;
using Nw.LiveReceptionManage.WebApi.JwtHelper;
using Nw.LiveReceptionManage.WebApi.Utility.Filter;
using Nw.LiveReceptionManage.WebApi.Utility.MiddlewareExtesnion;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Nw.LiveReceptionManage.WebApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nw.LiveReceptionManage.WebApi", Version = "v1" });
            });

            //���������������
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });


            //�����ļ�ע��
            services.AddConfig(Configuration);

            //ע�����ݿ����Ӷ���
            services.AddDbContextConfigure(Configuration);

            services.AddControllers(configure =>
            {
                //��ӹ�����
                configure.Filters.Add<CustomExceptionFilterAttribute>();
                configure.Filters.Add<CustomActionFilterAttribute>();
            });


            //�������
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            
            //jwt��֤
            JwtAuthentication.Validate(services, Configuration);

            //AutoMapper����
            services.AddAutoMapper(typeof(ServiceProfile));

            //����Hangfire
            services.AddHangfireConfigure(Configuration);

        }

        /// <summary>
        /// ����Autofac����ע��
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nw.LiveReceptionManage.WebApi v1"));
            //}

            if (Configuration.GetValue<bool>("SwaggerOpen"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nw.LiveReceptionManage.WebApi v1"));
            }

            //app.UseHttpsRedirection();



            //���֧�ֿ����м��
            app.UseCors();

            app.UseRouting();

            //ʹ���м������ʾͼƬ��������
            app.ShowImg(Configuration);

            //��Ӽ�Ȩ�м��
            app.UseAuthentication();

            app.UseAuthorization();

            //Hangfire 
            app.UseHangfireConfigure();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            //��ʼ��RabbitMQ���Ѷ�
            app.Subscribe();
        }
    }
}
