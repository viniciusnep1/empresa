using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Identity;
using seguranca;
using Microsoft.AspNetCore.Routing;
using Seguranca;
using web.Controllers;

namespace web
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        //Add any custom requirement properties if you have them
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class PermissionAttribute : AuthorizeAttribute
    {
        public string Name { get; }

        public PermissionAttribute(string name) : base("Permission")
        {
            Name = name;
        }
    }

    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var attributes = new List<TAttribute>();
            var attribute = default(TAttribute);
            RouteData routeData = null;
            var action = (context.Resource as AuthorizationFilterContext)?.ActionDescriptor as ControllerActionDescriptor;
            if (action != null)
            {
                routeData = (context.Resource as AuthorizationFilterContext)?.RouteData;
                attribute = GetAttributes(action.ControllerTypeInfo.UnderlyingSystemType).FirstOrDefault();
                attributes.AddRange(GetAttributes(action.MethodInfo));
            }

            return HandleRequirementAsync(context, routeData, requirement, attribute, attributes);
        }

        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, RouteData routeData, TRequirement requirement, TAttribute attribute, IEnumerable<TAttribute> attributes);

        private static IEnumerable<TAttribute> GetAttributes(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
        }
    }

    public class PermissionAuthorizationHandler : AttributeAuthorizationHandler<PermissionAuthorizationRequirement, PermissionAttribute>
    {
        UserManager<Usuario> userManager;

        public PermissionAuthorizationHandler(UserManager<Usuario> userManager)
        {
            this.userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RouteData routeData,  PermissionAuthorizationRequirement requirement, PermissionAttribute attribute, IEnumerable<PermissionAttribute> attributes)
        {
            Guid idUrl = Guid.Empty;
            if (routeData.Values.ContainsKey(Permissoes.ID_NOME_URL))
            {
                idUrl = Guid.Parse(routeData.Values[Permissoes.ID_NOME_URL].ToString());
            }

            foreach (var permissionAttribute in attributes)
            {
                if (!await AuthorizeAsync(context.User, idUrl, attribute.Name, permissionAttribute.Name))
                {
                    return;
                }
            }

            context.Succeed(requirement);
        }

        private async Task<bool> AuthorizeAsync(ClaimsPrincipal user, Guid fazendaId, string modulo, string permissaoModulo)
        {
            var usuario = await userManager.GetUserAsync(user);

#if DEBUG
            if (usuario == null)
                return true;
#endif

            var papeis = new List<string>();

            //usuario.Papeis.ForEach(papel => {
            //    papel.Role.Permissoes.ForEach(rolePermissao =>
            //    {
            //        var permissao = string.Format("{0}-{1}-{2}", papel.Role.Name, papel.FazendaId, rolePermissao.Permissao.Codigo);
            //        papeis.Add(permissaoModulo);
            //    });
            //});

            return true;
        }
    }

}
