using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using entities;

namespace seguranca
{
    [Table("usuario_token", Schema = Schema.SCHEMA_SEGURANCA)]
    public class UsuarioToken : IdentityUserToken<Guid>
    {

    }
}
