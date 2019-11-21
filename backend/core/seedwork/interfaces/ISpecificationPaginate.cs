using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace core.seedwork
{
    public interface ISpecificationPaginate<T> : ISpecification<T>
    {
        string Order { get; }
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
