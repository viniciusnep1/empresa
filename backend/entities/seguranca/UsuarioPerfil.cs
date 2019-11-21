using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using entities;
using entities.empresa;

namespace seguranca
{
    [Table("usuario_perfil", Schema = Schema.SCHEMA_SEGURANCA)]
    public class UsuarioPerfil : IdentityUserRole<Guid>
    {

        /// Usuário
        /// </summary>
        public virtual Usuario User { get; set; }

        /// <summary>
        /// Perfis
        /// </summary>
        public virtual Perfil Role { get; set; }
    }
}
