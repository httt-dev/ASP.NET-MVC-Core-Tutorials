using CS46.Webpack.Request.Cookie.Json.Upload.mylib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
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

                // path : cookies/write
                //        cookies/read

                // *action : * co nghia la khong co chuoi chuoi action cung duoc
                // vi du : /Cookies/
                //          /Cookies/abc  => action = abc
                //          /Cookies/zyx 
                endpoints.MapGet("/Cookies/{*action}", async (context) =>
                {
                    // la data ma server gui ve cho client
                    // khi trinh duyet co cookies thi se luu lai va dung cho cac lam truy van sau 
                    // khi da co cookies thi khi truy van client se gui kem thong tin cookies len server

                    var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);

                    // lay gia tri action tu route
                    var action = context.GetRouteValue("action")  ?? "read";

                    string message = "";
                    if (action.ToString() == "write")
                    {
                        // ten = gia tri cua cookie 
                        // thoi gian hieu luc

                        

                        var option = new CookieOptions
                        {
                            Path = "/",
                            Expires = DateTime.Now.AddDays(1)
                        };

                        context.Response.Cookies.Append("masanpham", "SP-ABC" , option);
                        message = "Cookies duoc ghi";

                    }
                    else
                    {
                        // lay danh sach cac cookies

                        var listCookies = context.Request.Cookies.Select((cookie) => $"{cookie.Key} : {cookie.Value}".HtmlTag("li"));

                        message = string.Join("", listCookies).HtmlTag("ul");
                    }

                    var huongdan = "<a href=\"/Cookies/read\"> Doc cookie</a><br><a href=\"/Cookies/write\"> Ghi cookie</a>";

                    huongdan = huongdan.HtmlTag("div", "container mt-4");
                    message = message.HtmlTag("div", "alert-danger");

                    var html = HtmlHelper.HtmlDocument("Thong tin Cookies : " + action, menu + huongdan + message);
                    //context.Request
                    var info = RequestProcess.RequestInfo(context.Request).HtmlTag("div", "container");

                    await context.Response.WriteAsync(html);

                });
                endpoints.MapGet("/Json", async (context) =>
                {
                   
                    var p = new
                    {
                        TenSP = "Dong ho garmin",
                        Gia = 100000,
                        NgaySX = new DateTime(2023,1,12)
                    };

                    context.Response.ContentType = "application/json";

                    var json = JsonConvert.SerializeObject(p);

                    await context.Response.WriteAsync(json);
                });
                endpoints.MapMethods("/Form", new string[] { "POST" , "GET"}, async (context) =>
                {
                    var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);

                    var formHtml = await RequestProcess.ProcessForm(context.Request);

                    var html = HtmlHelper.HtmlDocument("Form", menu+ formHtml);

                    //context.Request
                    var info = RequestProcess.RequestInfo(context.Request).HtmlTag("div", "container");

                    await context.Response.WriteAsync(html);
                });

                //endpoints.MapPost("/Form", async (context) =>
                //{
                //    var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);

                //    var formHtml = RequestProcess.ProcessForm(context.Request);

                //    var html = HtmlHelper.HtmlDocument("Form", menu + formHtml);

                //    //context.Request
                //    var info = RequestProcess.RequestInfo(context.Request).HtmlTag("div", "container");

                //    await context.Response.WriteAsync(html);
                //});

            });
        }
    }
}
