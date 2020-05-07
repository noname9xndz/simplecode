using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleApp.Extensions;
using ModuleApp.Infrastructure.Common;
using ModuleApp.Module.Core.Data;
using System;
using System.IO;

namespace ModuleApp
{
    public class MigrationModuleAppDbContextFactory : IDesignTimeDbContextFactory<ModuleAppDbContext>
    {
        public ModuleAppDbContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var contentRootPath = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(contentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", true);

            builder.AddUserSecrets(typeof(MigrationModuleAppDbContextFactory).Assembly, optional: true);
            builder.AddEnvironmentVariables();
            var _configuration = builder.Build();

            //setup DI
            IServiceCollection services = new ServiceCollection();
            GlobalConfiguration.ContentRootPath = contentRootPath;
            services.AddModules(contentRootPath);
            services.AddCustomizedDataStore(_configuration);
            var _serviceProvider = services.BuildServiceProvider();

            return _serviceProvider.GetRequiredService<ModuleAppDbContext>();
        }
    }
}