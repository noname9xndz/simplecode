using Autofac;
using Autofac.Extensions.DependencyInjection;
using Basket.Infrastructure.Models.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Infrastructure.Ioc
{
    public static class NativeInjectorBootStrapper
    {
        public static IServiceProvider RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddCustomGrpc()
                //.AddAppInsight(configuration)
                .AddCustomMVC(configuration)
                .AddSwagger(configuration)
               // .AddJwtSecurity(configuration)
              // .AddCustomHealthCheck(configuration)
                .AddCustomOptions(configuration)
                .AddRedisService(configuration)
                .AddServiceBus(configuration)
                .AddEventBus(configuration)
                .AddCustomService()
                .AddOptions();
                

            var container = new ContainerBuilder();
            container.Populate(services);
            
            return new AutofacServiceProvider(container.Build());
        }

        public static void RegisterApplication(IApplicationBuilder app,IConfiguration configuration)
        {
            CustomApplicationExtension.AddCustomSwagger(app, configuration);

            app.UseRouting();
            app.UseCors("CorsPolicy");

            CustomApplicationExtension.AddAuthCustom(app, configuration);

            app.UseStaticFiles();

            CustomApplicationExtension.AddEndpoints(app).AddEventBus();
        }

    }
}
