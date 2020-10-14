﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Weapsy.Mvc.Apps
{
    public abstract class StartupBase : IStartup
    {
        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        public virtual void Configure(IApplicationBuilder builder)
        {
        }
    }
}
