using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;

namespace core.seedwork.interfaces
{
    public interface IUser
    {
        string Name { get; }

        bool IsAuthenticated();

        IEnumerable<Claim> GetClaimsIdentity();

    }
}
