using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_50
{
    public class Program
    {  
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Debug()
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                  .Enrich.FromLogContext()
                  .WriteTo.Console()
                  .WriteTo.File(
                @"D:\logs\dot50_.log",
                fileSizeLimitBytes: 1_000_000,
                rollOnFileSizeLimit: true,
                rollingInterval:RollingInterval.Hour,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1))
                  //.WriteTo.Fluentd("127.0.0.1", 24224, "test2.xxxx")
                  .CreateLogger(); 
            try
            {
                logger.Information("±âµ¿");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch(Exception exc)
            {
                Log.Fatal(exc, "The Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            } 
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                //.UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                //.ReadFrom.Configuration(                    
                //    hostingContext.Configuration)                
                //.Enrich.FromLogContext()
                //) //Uses Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        
    }
}
