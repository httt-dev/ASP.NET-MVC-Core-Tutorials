using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewarePipeline
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // can phai dang ky Middleware
            services.AddSingleton<SecondMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // PIPELINE : Static - First - Second - EndPoints Middleware (enpoint 1 / endpoint2) - Terminal middleware

            app.UseStaticFiles(); // sau khi xu ly middleware nay thi se ket thuc luon

            // dang ky middleware voi app
            //app.UseMiddleware<FirstMiddleware>();

            app.UseFirstMiddleware();

            app.UseSecondMiddleware();

            app.UseRouting();
            // tao cac endpoint ( terminal middleware)
            app.UseEndpoints((endpoint) =>
            {
                // end point 1
                endpoint.MapGet("/about.html", async (context) =>
                {
                    await context.Response.WriteAsync("Trang gioi thieu");
                });

                // End point 2
                endpoint.MapGet("/sanpham.html", async (context) =>
                {
                    await context.Response.WriteAsync("Trang san pham");
                });

            });

            // RE NHANH PIPELINE
            app.Map("/admin", (appAdmin) =>
            {
                // Tao middleware cua nhanh 

                // co the them cac middleware khac tai day giong nhu tren 

                // Cai nay la mot terminal pipeline (pipeline cuoi cung cua nhanh, cung khong goi cac middleware khac nua )
                // Khi gap pipeline nay thi se tra ve cho client (khong goi cac middleware khac nua)
                app.Run(async (context) =>
                {
                    await context.Response.WriteAsync($"Xin chao ASP.NET CORE , toi la ADMIN");
                });


            });

            // Cai nay la mot terminal pipeline (pipeline cuoi cung)
            // Khi gap pipeline nay thi se tra ve cho client (khong goi cac middleware khac nua)
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Xin chao ASP.NET CORE");
            });


            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

        }
    }
}
