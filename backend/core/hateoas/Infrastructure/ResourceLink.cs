using Microsoft.AspNetCore.Routing;
using System;

namespace hateoas.infrastructure
{
    public class ResourceLink<T> : ILinksRequirement
    {
        public ResourceLink(Type resourceType, string name, Func<T, RouteValueDictionary> values)
        {
            ResourceType = resourceType;
            Name = name;
            Values = values;
        }

        public ResourceLink(Type resourceType, string name, Func<T, RouteValueDictionary> values, string permission = "")
        {
            ResourceType = resourceType;
            Name = name;
            Values = values;
            Permission = permission;
        }

        public string Name { get; }
        public Func<T, RouteValueDictionary> Values { get; }
        public Type ResourceType { get; }
        public string Permission { get; }

        public object RouteValues(object input) => Values?.Invoke((T)input);
    }
}
