using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Basket.Infrastructure.Extensions;
using Basket.Infrastructure.Models.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using TestMicro.Infrastructure.Extensions;

namespace Basket.Api
{
    public class Program : BasketProgram
    {
        public static async Task<int> Main(string[] args)
        {
            var configuration = CustomProgramExtension.GetConfiguration();

            var appName = CustomProgramExtension.AppName;

            Log.Logger = CustomProgramExtension.CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", appName);
                var host = CreateHostBuilder(configuration, args);

                Log.Information("Starting web host ({ApplicationContext})...", appName);

                await host.Build().RunAsync();

                return 0;

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", appName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.CaptureStartupErrors(false)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        //.ConfigureKestrel(options =>
                        //{
                        //    var ports = CustomProgramExtension.GetDefinedPorts(configuration);
                        //    options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
                        //    {
                        //        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        //    });

                        //    options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
                        //    {
                        //        listenOptions.Protocols = HttpProtocols.Http2;
                        //    });

                        //})
                        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                        .UseFailing(options =>
                        {
                            options.ConfigPath = "/Failing";
                            options.NotFilteredPaths.AddRange(new[] { "/hc", "/liveness" });
                        })
                        .UseStartup<Startup>()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        //.UseWebRoot("Pics")
                        .UseSerilog();
                })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
