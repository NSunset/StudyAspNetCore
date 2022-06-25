using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nw.gRPC.Framework;
using Nw.gRPC.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Nw.gRPC.Client
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nw.gRPC.Client", Version = "v1" });
            });

            //解决JsonResult中文乱码问题
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });

            //实现GRPC依赖注入
            services.AddGrpcClient<UserGrpc.UserGrpcClient>(options =>
            {
                options.Address = new Uri("https://localhost:5001");


                //实现客户端访问GRPC AOP
                ServiceProvider serviceProvider = services.BuildServiceProvider();
                ILogger<CustomClientInterceptor> log = serviceProvider.GetService<ILogger<CustomClientInterceptor>>();
                options.Interceptors.Add(new CustomClientInterceptor(log));

                
            }).ConfigureChannel(options=> {

                CallCredentials callCredentials = CallCredentials.FromInterceptor(async (context, metadata) => {
                    //token可以使用缓存，在获取
                    string token =await GetToken();
                    metadata.Add("Authorization", $"Bearer {token}");
                });
                options.Credentials = ChannelCredentials.Create(new SslCredentials(), callCredentials);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nw.gRPC.Client v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private async Task<string> GetToken()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent content = new StringContent("");

                HttpResponseMessage responseMessage = await httpClient.PostAsync("https://localhost:5003/api/User/Login?userName=张三&password=123456", content);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception("获取token失败");
                }
                string token = await responseMessage.Content.ReadAsStringAsync();
                return token;
            }
        }
    }
}
