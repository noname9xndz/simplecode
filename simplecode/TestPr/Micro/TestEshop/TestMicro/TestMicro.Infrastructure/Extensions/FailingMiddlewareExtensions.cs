using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using TestMicro.Infrastructure.Middlewares;
using TestMicro.Infrastructure.Models;

namespace TestMicro.Infrastructure.Extensions
{
    public static class FailingMiddlewareExtensions
    {
        public static IApplicationBuilder UseFailingMiddleware(this IApplicationBuilder builder)
        {
            return UseFailingMiddleware(builder, null);
        }
        public static IApplicationBuilder UseFailingMiddleware(this IApplicationBuilder builder, Action<FailingOptions> action)
        {
            var options = new FailingOptions();
            action?.Invoke(options);
            builder.UseMiddleware<FailingMiddleware>(options);
            return builder;
        }
    }
}
