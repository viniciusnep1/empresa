using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace core.extensions
{
    public static class JwtUserExtensions
    {
        public static async Task<TUser> GetJwtUserAsync<TUser>(this UserManager<TUser> manager, ClaimsPrincipal principal) where TUser : class
        {
            var value = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await manager.FindByNameAsync(value);
            return user;
        }
    }
}
