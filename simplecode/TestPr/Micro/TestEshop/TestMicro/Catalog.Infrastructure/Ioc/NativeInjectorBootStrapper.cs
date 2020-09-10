﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Extensions;
using Catalog.Infrastructure.Models.Base;
using EventLogEF.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;

namespace Catalog.Infrastructure.Ioc
{
    public static class NativeInjectorBootStrapper
    {
        public static IServiceProvider RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                //.AddAppInsight(configuration)
                //.AddGrpc().Services
                .AddCustomMVC(configuration)
                .AddCustomDbContext(configuration)
                .AddCustomOptions(configuration)
                .AddServiceBus(configuration)
                .AddEventBus(configuration)
                .AddSwagger(configuration);
            // .AddCustomHealthCheck(configuration);

            var container = new ContainerBuilder();
            container.Populate(services);

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.MigrateDbContext<CatalogDbContext>((context, service) =>
            {
                Log.Information("Applying migrations ({ApplicationContext})...", CustomProgramExtension.AppName);

                var env = service.GetService<IWebHostEnvironment>();
                var settings = service.GetService<IOptions<CatalogSettings>>();
                var logger = service.GetService<ILogger<CatalogContextSeed>>();

                new CatalogContextSeed().SeedAsync(context, env, settings, logger).Wait();
            }).MigrateDbContext<EventLogDbContext>((_, __) => { });

            return new AutofacServiceProvider(container.Build());
        }

        public static void RegisterApplication(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            var pathBase = configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<CatalogStartUp>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Catalog.API V1");
                });

            app.UseRouting();

            // app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                //endpoints.MapGet("/_proto/", async ctx =>
                //{
                //    ctx.Response.ContentType = "text/plain";
                //    using var fs = new FileStream(Path.Combine(env.ContentRootPath, "Proto", "catalog.proto"), FileMode.Open, FileAccess.Read);
                //    using var sr = new StreamReader(fs);
                //    while (!sr.EndOfStream)
                //    {
                //        var line = await sr.ReadLineAsync();
                //        if (line != "/* >>" || line != "<< */")
                //        {
                //            await ctx.Response.WriteAsync(line);
                //        }
                //    }
                //});
                //endpoints.MapGrpcService<CatalogService>();
                //endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                //{
                //    Predicate = _ => true,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});
                //endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                //{
                //    Predicate = r => r.Name.Contains("self")
                //});
            });

            // ConfigureEventBus(app);
        }
    }
}