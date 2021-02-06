using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Application Starting.");
                using IHost host = CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>()
                        .CaptureStartupErrors(true)
                        .ConfigureAppConfiguration(config =>
                        {
                            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                            config
                                .AddJsonFile("appsettings.json")
                                // Used for Hosting Environment settings.
                                .AddJsonFile($"appsettings.{env}.json", optional: true)
                                // Used for local settings like connection strings.
                                .AddJsonFile("appsettings.Local.json", optional: true)
                                // Used for log settings like serilog strings.
                                .AddJsonFile("appsettings.log.json", optional: true, reloadOnChange: true)
                                // Used for local settings like PayPal strings.
                                .AddJsonFile("appsettings.Paypal.json", optional: true, reloadOnChange: true)
                                .AddEnvironmentVariables();
                        })
                        .UseSerilog((hostingContext, loggerConfiguration) =>
                        {
                            loggerConfiguration
                                .ReadFrom.Configuration(hostingContext.Configuration, sectionName: "Slog")
                                // To get the specific log data
                                .WriteTo.Map("Name", (name, wt) => wt.File($"./Logs/log-{name}.log"), sinkMapCountLimit: 10)
                                .WriteTo.Logger(c =>
                                    c.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                                        .WriteTo.File("./Logs/Warning.log"))
                                .WriteTo.Logger(c =>
                                    c.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                                        .WriteTo.File("./Logs/Error.log"))
                                .WriteTo.Logger(c =>
                                    c.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
                                        .WriteTo.File("./Logs/Fatal.log"))
                                .Enrich.FromLogContext()
                                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                                .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);

                        });
                   });


        // Old Config in Program.cs class
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
