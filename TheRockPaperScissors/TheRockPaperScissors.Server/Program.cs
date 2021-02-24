using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TheRockPaperScissors.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logginBuilder =>
                {
                    logginBuilder.ClearProviders();
                    logginBuilder.SetMinimumLevel(LogLevel.Trace);
                    logginBuilder.AddSerilog(new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.File("service.log")
                        .CreateLogger());
                });
    }
}
