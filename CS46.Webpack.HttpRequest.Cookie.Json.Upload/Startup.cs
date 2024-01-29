using CS46.Webpack.Request.Cookie.Json.Upload.mylib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS46.Webpack.Request.Cookie.Json.Upload
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); 

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var menu = HtmlHelper.MenuTop(
                        //new object[]
                        //{
                        //    new
                        //    {
                        //        url = "/abc",
                        //        label = "Menu ABC"
                        //    },
                        //    new
                        //    {
                        //        url = "/xyz",
                        //        label = "Menu XYZ"
                        //    }
                        //}
                        HtmlHelper.DefaultMenuTopItems()
                        , context.Request
                        );

                    var html = HtmlHelper.HtmlDocument("XIN CHAO", menu + HtmlHelper.HtmlTrangchu());

                    await context.Response.WriteAsync(html);
                });

                endpoints.MapGet("/RequestInfo", async (context) =>
                {
                    var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);

                    var html = HtmlHelper.HtmlDocument("Thong tin Request", menu );
                    //context.Request
                    var info = RequestProcess.RequestInfo(context.Request).HtmlTag("div","container");

                    await context.Response.WriteAsync(html + info );

                });

                endpoints.MapGet("/Encoding", async (context) =>
                {
                    await context.Response.WriteAsync("Encoding");
                });

                endpoints.MapGet("/Cookies", async (context) =>
                {
                    await context.Response.WriteAsync("Cookies");
                });
                endpoints.MapGet("/Json", async (context) =>
                {
                    await context.Response.WriteAsync("Json");
                });
                endpoints.MapGet("/Form", async (context) =>
                {
                    await context.Response.WriteAsync("Form");
                });

            });
        }
    }
}
