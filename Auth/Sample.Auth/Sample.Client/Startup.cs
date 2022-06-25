using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Sample.Common;
using Sample.Common.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sample.Common.JwtHelpers;
using Sample.Client.Utility;
using Microsoft.AspNetCore.Authorization;

namespace Sample.Client
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample.Client", Version = "v1" });
            });
            var jwtConfigure = new JwtConfigure();
            Configuration.GetSection("JwtConfigure").Bind(jwtConfigure);

            //对称可逆加密方式
            //services.AddHSJwtAuthentication(jwtConfigure);

            //非对称可逆加密方式
            services.AddRSJwtAuthentication(jwtConfigure);

            services.AddAuthorization(options =>
            {
                //自定义策略，在鉴权验证完有用户信息后，这里再来做验证
                options.AddPolicy("CustomPolicy", builder =>
                {
                    builder.Requirements.Add(new PathRequirement());
                });
            });

            //具体的验证操作，注入容器
            services.AddSingleton<IAuthorizationHandler, PathAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample.Client v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
