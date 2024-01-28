using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Asp.netIntroduction
{
    /// <summary>
    /// Muon khai bao la 1 startUp thi can phai khai bao 2 phuong thuc sau
    /// 
    /// </summary>
    public class MyStartup
    {
        /// <summary>
        /// Dang ky cac dich vu cua ung dung
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

        }

        /// <summary>
        /// Xay dung pipeline de xu ky request 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // wwwroot folder 
            // khi gap middleware nay thi yeu cau phai co thu muc wwwroot ,
            // co the thay doi thu muc nay cach thay doi cau hinh cua IwebHostBuilder.UseWebRoot() <- file startup.cs
            //  
            // neu co tai nguyen can truy cap thi no tra ve va dung cac pipeline khac
            // vi tri : nen viet xu ly nay o dau , neu viet o vi tri khac thi co the se bi xu ly boi cac middleware khac truoc
            app.UseStaticFiles();

            // Request 
            //EndpointRoutingMiddleware
            app.UseRouting();

            // dinh nghia cac endpoint 

            app.UseEndpoints(endpoints =>
            {
                // GET 
                endpoints.MapGet("/", async (context) =>
                {

                    string html = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <title>Trang web đầu tiên</title>
                    <link rel=""stylesheet"" href=""/css/bootstrap.min.css"" />
                    <script src=""/js/jquery.min.js""></script>
                    <script src=""/js/popper.min.js""></script>
                    <script src=""/js/bootstrap.min.js""></script>


                </head>
                <body>
                    <nav class=""navbar navbar-expand-lg navbar-dark bg-danger"">
                            <a class=""navbar-brand"" href=""#"">Brand-Logo</a>
                            <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#my-nav-bar"" aria-controls=""my-nav-bar"" aria-expanded=""false"" aria-label=""Toggle navigation"">
                                    <span class=""navbar-toggler-icon""></span>
                            </button>
                            <div class=""collapse navbar-collapse"" id=""my-nav-bar"">
                            <ul class=""navbar-nav"">
                                <li class=""nav-item active"">
                                    <a class=""nav-link"" href=""#"">Trang chủ</a>
                                </li>
                            
                                <li class=""nav-item"">
                                    <a class=""nav-link"" href=""#"">Học HTML</a>
                                </li>
                            
                                <li class=""nav-item"">
                                    <a class=""nav-link disabled"" href=""#"">Gửi bài</a>
                                </li>
                        </ul>
                        </div>
                    </nav> 
                    <p class=""display-4 text-danger"">Đây là trang đã có Bootstrap</p>
                </body>
                </html>
    ";
                    await context.Response.WriteAsync(html);

                });
                endpoints.MapGet("/about.html", async (context) =>
                {
                    await context.Response.WriteAsync("Trang about");
                });


            });

            // Trong IApplicationBuilder thi co cung cap san mot so terminal middleware de chung ta co the su dung
            // Mot loai terminal khac 
            // neu dia chi url la trung khop thi se chay cac pipe dinh nghia ben trong 

            app.Map("/abc", (app1) =>
            {
                app1.Run(async (context) =>
                {
                    await context.Response.WriteAsync("/abc");
                });
            });

            // terminal middleware
            app.Run(async (HttpContext context) =>
            {
                await context.Response.WriteAsync("Xin chào đây là MyStartup");
            });

            //middleware de xu ly khi khong tim thay endpoint nao xu ly
            app.UseStatusCodePages();
        }
    }
}