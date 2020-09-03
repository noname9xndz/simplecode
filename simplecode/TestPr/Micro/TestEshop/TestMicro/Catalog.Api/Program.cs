using System;
using System.IO;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Extensions;
using Catalog.Infrastructure.Ioc;
using Catalog.Infrastructure.Models.Base;
using EventLogEF.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using TestMicro.Infrastructure.Extensions;

namespace Catalog.Api
{
    public class Program : CatalogProgram
    {
        

        public static int Main(string[] args)
        {
            //  CreateHostBuilder(args).Build().Run();
            var configuration = CustomProgramExtension.GetConfiguration();

            Log.Logger = CustomProgramExtension.CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", CustomProgramExtension.AppName);
                var host = CreateHostBuilder(configuration, args);

                Log.Information("Applying migrations ({ApplicationContext})...", CustomProgramExtension.AppName);

                host.MigrateDbContext<CatalogDbContext>((context, services) =>
                    {
                       var env = services.GetService<IWebHostEnvironment>();
                       var settings = services.GetService<IOptions<CatalogSettings>>();
                       var logger = services.GetService<ILogger<CatalogContextSeed>>();

                        new CatalogContextSeed()
                            .SeedAsync(context, env, settings, logger)
                            .Wait();
                    })
                    .MigrateDbContext<EventLogDbContext>((_, __) => { });

                Log.Information("Starting web host ({ApplicationContext})...", CustomProgramExtension.AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", CustomProgramExtension.AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        //        public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
        //            Host.CreateDefaultBuilder(args)
        //                .ConfigureWebHostDefaults(webBuilder =>
        //                {
        //                    webBuilder
        //                        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
        //                        .UseStartup<Startup>()
        //                        .UseContentRoot(Directory.GetCurrentDirectory())
        //                        //.UseWebRoot("Pics")
        //                        .UseSerilog()
        //                        .Build();
        //                });
        private static IWebHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .CaptureStartupErrors(false)
//                .ConfigureKestrel(options =>
//                {
//                    var ports = CustomProgramExtension.GetDefinedPorts(configuration);
//                    options.Listen(System.Net.IPAddress.Any, ports.httpPort, listenOptions =>
//                    {
//                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
//                    });
//                    options.Listen(System.Net.IPAddress.Any, ports.grpcPort, listenOptions =>
//                    {
//                        listenOptions.Protocols = HttpProtocols.Http2;
//                    });
//
//                })
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseWebRoot("Pics")
                .UseSerilog()
                .Build();
    }
}
