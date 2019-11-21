using System;
using System.ComponentModel.DataAnnotations.Schema;
using entities;
using Microsoft.AspNetCore.Identity;

namespace seguranca
{
    [Table("usuario_claim", Schema = Schema.SCHEMA_SEGURANCA)]
    public class UsuarioClaim : IdentityUserClaim<Guid>
    {

    }
}
