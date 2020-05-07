using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ModuleApp.Infrastructure.Common;
using ModuleApp.Infrastructure.Modules;
using ModuleApp.Module.News.Services.Implementation;
using ModuleApp.Module.News.Services.Interfaces;
using SimplCommerce.Module.News.Services;

namespace ModuleApp.Module.News
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<INewsItemService, NewsItemService>();
            services.AddTransient<INewsCategoryService, NewsCategoryService>();

            GlobalConfiguration.RegisterAngularModule("ModuleApp.news");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}