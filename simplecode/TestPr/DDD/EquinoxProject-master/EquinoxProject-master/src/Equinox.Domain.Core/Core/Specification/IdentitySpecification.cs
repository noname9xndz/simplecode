using System;
using System.Linq.Expressions;
using Equinox.Domain.Core.Core.Specification;

namespace Equinox.Domain.Core.Core.Specification
{
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }
}