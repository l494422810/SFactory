using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Component;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Config
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            IConfiguration config = AppConfigurtaion.BuilderConfiguration("serviceregister.json");
            var obj = config.GetSection("Config:Register").GetChildren().ToList();
            List<AppService.ServiceAddress> sa = new List<AppService.ServiceAddress>();
            for (int i = 0; i < obj.Count; i++)
            {
                sa.Add(new AppService.ServiceAddress() { Libraries = obj[i].Key, ServiceName = obj[i].Value });
            }
            AppService.RegisterService(Convert.ToInt32(AppConfigurtaion.AppSettings.GetSection("ServicePort").Value), AppService.GetNetAddress(), sa);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
