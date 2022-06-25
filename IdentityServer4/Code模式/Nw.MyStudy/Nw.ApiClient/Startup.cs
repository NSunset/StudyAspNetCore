using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.ApiClient
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nw.ApiClient", Version = "v1" });
            });

            #region Identity��Ȩ����


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
           {
               options.Authority = "https://localhost:5001";
               //options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = "https://localhost:5001",
                   ValidateAudience = true,
                   ValidAudience = "https://localhost:5001/resources",
                   ValidateIssuerSigningKey = true
               };


               options.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(10);//Ĭ��5������֤һ��
               options.TokenValidationParameters.RequireExpirationTime = true;//��֤Token�Ƿ����
           });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nw.ApiClient v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            #region ʹ�ü�Ȩ�м��
            app.UseAuthentication();


            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                //Ĭ��ȫ��ע��Authorize
                .RequireAuthorization()
                ;
            });
        }
    }
}
