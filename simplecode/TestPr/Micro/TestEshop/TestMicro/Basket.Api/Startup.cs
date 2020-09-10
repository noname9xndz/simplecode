using Basket.Infrastructure.Ioc;
using Basket.Infrastructure.Models.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Api
{
    public class Startup : BasketStartUp
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            NativeInjectorBootStrapper.RegisterApplication(app, Configuration);
        }
    }
}