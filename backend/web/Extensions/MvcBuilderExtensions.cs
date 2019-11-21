using hateoas.formatters;
using hateoas.infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace web.Extensions
{
    public static class MvcBuilderExtensions
    {
        //public static IMvcBuilder AddHateoas(this IMvcBuilder builder, Action<HateoasOptions> options = null)
        //{
        //    if (options != null)
        //    {
        //        builder.Services.Configure(options);
        //    }
        //    builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        //    builder.AddMvcOptions(o => o.OutputFormatters.Add(new JsonHateoasFormatter()));
        //    return builder;
        //}
    }
}
