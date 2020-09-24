using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLGraphTypeFirstNestedTable.GraphQL.Extensions
{
    public static class GraphQLExtension
    {
        public static class ContextExtensions
        {
            public static async Task<TReturn> RunScopedAsync<TSource, TReturn>(IResolveFieldContext<TSource> context,
                Func<IResolveFieldContext<TSource>, IServiceProvider, Task<TReturn>> func)
            {
                using (var scope = context.RequestServices.CreateScope())
                {
                    return await func(context, scope.ServiceProvider);
                }
            }
        }

        public static class FieldBuilderExtensions
        {
            //public static FieldBuilder<TSource, TReturn> ResolveScopedAsync<TSource, TReturn>(FieldBuilder<TSource, TReturn> builder,
            //    Func<IResolveFieldContext<TSource>, IServiceProvider, Task<TReturn>> func)
            //{
            //    return builder.ResolveAsync(context => context.RunScopedAsync(func));
            //}
        }
    }
}
