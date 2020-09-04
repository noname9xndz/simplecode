using System;
using System.IO;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Extensions;
using Catalog.Infrastructure.Ioc;
using Catalog.Infrastructure.Models.Base;
using EventLogEF.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using TestMicro.Infrastructure.Extensions;

namespace Catalog.Api
{
    public class Program : CatalogProgram
    {
        public static async Task<int> Main(string[] args)
        {
            //  CreateHostBuilder(args).Build().Run();
            var configuration = CustomProgramExtension.GetConfiguration();

            Log.Logger = CustomProgramExtension.CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", CustomProgramExtension.AppName);
                var host = CreateHostBuilder(configuration, args);


                //host.Build().Run();

              //  host.Build().MigrateDbContext<CatalogDbContext>((context, services) =>
//                host.MigrateDbContext<CatalogDbContext>((context, services) =>
//                    {
//                        var env = services.GetService<IWebHostEnvironment>();
//                        var settings = services.GetService<IOptions<CatalogSettings>>();
//                        var logger = services.GetService<ILogger<CatalogContextSeed>>();
//
//                        new CatalogContextSeed().SeedAsync(context, env, settings, logger).Wait();
//                    })
//                    .MigrateDbContext<EventLogDbContext>((_, __) => { });
                
                Log.Information("Starting web host ({ApplicationContext})...", CustomProgramExtension.AppName);
                host.Build().Run();

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

        public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                        .UseStartup<Startup>()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        //.UseWebRoot("Pics")
                        .UseSerilog();
                })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());
//        private static IWebHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
//            WebHost.CreateDefaultBuilder(args)
//               // .UseDefaultServiceProvider(new AutofacServiceProviderFactory())
//                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
//                .CaptureStartupErrors(false)
////                .ConfigureKestrel(options =>
////                {
////                    var ports = CustomProgramExtension.GetDefinedPorts(configuration);
////                    options.Listen(System.Net.IPAddress.Any, ports.httpPort, listenOptions =>
////                    {
////                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
////                    });
////                    options.Listen(System.Net.IPAddress.Any, ports.grpcPort, listenOptions =>
////                    {
////                        listenOptions.Protocols = HttpProtocols.Http2;
////                    });
////
////                })
//                .UseStartup<Startup>()
//                .UseContentRoot(Directory.GetCurrentDirectory())
//                //.UseWebRoot("Pics")
//                .UseSerilog()
//                .Build();
    }
}
