using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace core.seedwork
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        [JsonIgnore]
        public List<Expression<Func<T, bool>>> Criterias { get; } = new List<Expression<Func<T, bool>>>();

        [JsonIgnore]
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        [JsonIgnore]
        public List<string> IncludeStrings { get; } = new List<string>();

        protected BaseSpecification()
        {

        }

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criterias.Add(criteria);
        }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }      

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }



        public virtual void Build()
        {

        }
    }
}
