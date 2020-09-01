using System;
using AutoMapper;
using Equinox.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.Services.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}