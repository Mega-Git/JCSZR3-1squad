using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Core.Models;
using Serilog;
//using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Crypto.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Log.Logger = new LoggerConfiguration()
            //     .WriteTo.File("Cryptolog.txt", shared: true,
            //         outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            //     .CreateLogger();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Test.csv", shared: true, 
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u4} {Message:lj}{NewLine}{Exception}").MinimumLevel.Information()
                .CreateLogger();
            JsonFile.InitializeCurrienciesListFromFile();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(opt =>
                {
                    opt.ClearProviders();
                    opt.AddConsole();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
