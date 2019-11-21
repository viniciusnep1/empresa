using System;

namespace hateoas.infrastructure
{
    public interface ILinksRequirement
    {
        string Name { get; }
        object RouteValues(object input);
        Type ResourceType { get; }
        string Permission { get; }
    }
}

