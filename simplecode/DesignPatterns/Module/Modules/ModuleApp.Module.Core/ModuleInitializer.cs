using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ModuleApp.Infrastructure.Common;
using ModuleApp.Infrastructure.Modules;
using ModuleApp.Module.Core.Services.Implementation;
using ModuleApp.Module.Core.Services.Interfaces;

namespace ModuleApp.Module.Core
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEntityService, EntityService>();
            serviceCollection.AddTransient<IMediaService, MediaService>();
            serviceCollection.AddTransient<ILocalStorageService, LocalStorageService>();
            serviceCollection.AddTransient<IEmailSender, EmailSender>();
            //            serviceCollection.AddTransient<IThemeService, ThemeService>();
            //            serviceCollection.AddTransient<IWidgetInstanceService, WidgetInstanceService>();
            //            serviceCollection.AddScoped<IWorkContext, WorkContext>();
            //            serviceCollection.AddScoped<ISmsSender, SmsSender>();
            //            serviceCollection.AddSingleton<SettingDefinitionProvider>();
            //            serviceCollection.AddScoped<ISettingService, SettingService>();
            //            serviceCollection.AddScoped<ICurrencyService, CurrencyService>();

            GlobalConfiguration.RegisterAngularModule("ModuleApp.core");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}