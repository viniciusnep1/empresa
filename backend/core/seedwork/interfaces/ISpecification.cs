using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace core.seedwork
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, bool>>> Criterias { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        void Build();
    }
}
