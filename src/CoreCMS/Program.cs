using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Serilog;
using Serilog.Events;

namespace CoreCMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Debug Serilog for logging
            //Serilog.Debugging.SelfLog.Enable(msg =>
            //{
            //    Debug.WriteLine(msg);
            //});

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseSerilog((context, config) =>
                        {
                            config.ReadFrom.Configuration(context.Configuration);
                        });
                });
    }
}
