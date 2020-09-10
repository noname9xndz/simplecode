using Autofac.Extensions.DependencyInjection;
using Catalog.Infrastructure.Extensions;
using Catalog.Infrastructure.Models.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Catalog.Api
{
    public class Program : CatalogProgram
    {
        public static async Task<int> Main(string[] args)
        {
            var configuration = CustomProgramExtension.GetConfiguration();

            Log.Logger = CustomProgramExtension.CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", CustomProgramExtension.AppName);
                var host = CreateHostBuilder(configuration, args);
                Log.Information("Starting web host ({ApplicationContext})...", CustomProgramExtension.AppName);
                await host.Build().RunAsync();

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
    }
}