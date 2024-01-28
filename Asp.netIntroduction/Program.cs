using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.netIntroduction;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CS44.Asp.netIntroduction
{
    /* 
        Cau truc co ban cua ung dung web
        Host (IHost ) object 
            - Dependency Injection : IServiceProvider ( ServiceCollection)
            - Logging ( ILogging)
            - Configuration 
            - IHostedService => StartAsync: chay may chu Http ( Kestrel) 

        1. Tao IHostBuilder 
        2. Cau hinh , dang ky cac dich vu ( goi ConfigureWebHostDefaults)
        3. IHoBuilder.Build() => Host(IHost)
        4. Host.Run()

        Trong web app thi cac request phai duoc xu ly , do vay can phai cau hinh 
        de xu ly cac req .( doan code nao se xu ly request => tra ve response)
        Request => pipeline (middleware)
    */
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start App");
            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureWebHostDefaults((webBuilder) =>
            {
                // cau hinh mac dinh cho Host tao ra
                webBuilder.UseStartup<MyStartup>();

                // thay doi thu muc wwwroot ( static file)
                //webBuilder.UseWebRoot("public");
            });

            IHost host = builder.Build();
            host.Run();
        }
        // public static void Main(string[] args)
        // {
        //     CreateHostBuilder(args).Build().Run();
        // }

        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder.UseStartup<Startup>();
        //         });
    }
}
