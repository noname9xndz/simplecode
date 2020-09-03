using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalog.Infrastructure.Extensions
{
    public static class LinqSelectExtensions
    {
        public static IEnumerable<SelectTryResult<TSource, TResult>> SelectTry<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, TResult> selector)
        {
            foreach (TSource element in enumerable)
            {
                SelectTryResult<TSource, TResult> returnedValue;
                try
                {
                    returnedValue = new SelectTryResult<TSource, TResult>(element, selector(element), null);
                }
                catch (System.Exception ex)
                {
                    returnedValue = new SelectTryResult<TSource, TResult>(element, default(TResult), ex);
                }
                yield return returnedValue;
            }
        }

        public static IEnumerable<TResult> OnCaughtException<TSource, TResult>(this IEnumerable<SelectTryResult<TSource, TResult>> enumerable, Func<System.Exception, TResult> exceptionHandler)
        {
            return enumerable.Select(x => x.CaughtException == null ? x.Result : exceptionHandler(x.CaughtException));
        }

        public static IEnumerable<TResult> OnCaughtException<TSource, TResult>(this IEnumerable<SelectTryResult<TSource, TResult>> enumerable, Func<TSource, System.Exception, TResult> exceptionHandler)
        {
            return enumerable.Select(x => x.CaughtException == null ? x.Result : exceptionHandler(x.Source, x.CaughtException));
        }

        public class SelectTryResult<TSource, TResult>
        {
            internal SelectTryResult(TSource source, TResult result, System.Exception exception)
            {
                Source = source;
                Result = result;
                CaughtException = exception;
            }

            public TSource Source { get; private set; }
            public TResult Result { get; private set; }
            public System.Exception CaughtException { get; private set; }
        }
    }
}
