using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Catalog.Infrastructure.Models.Base;
using Event.Bus.Services.Base.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                //.AddIntegrationServices(configuration)
                // .AddEventBus(configuration)
                .AddSwagger(configuration);
               // .AddCustomHealthCheck(configuration);

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        public static void RegisterApplications(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
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


        private static  void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
           // eventBus.Subscribe<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
           // eventBus.Subscribe<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();
        }
    }
}
