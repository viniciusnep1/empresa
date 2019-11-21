using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using entities;

namespace seguranca
{
    [Table("perfil_claim", Schema = Schema.SCHEMA_SEGURANCA)]
    public class PerfilClaim : IdentityRoleClaim<Guid>
    {
    }
}
