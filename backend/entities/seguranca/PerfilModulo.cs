using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using entities;
using Newtonsoft.Json;
using core.seedwork;
using System.Collections.Generic;

namespace seguranca
{
    /// <summary>
    /// Modulo
    /// </summary>
    [Table("perfil_modulo", Schema = Schema.SCHEMA_SEGURANCA)]
    public class PerfilModulo : EntidadeBase
    {
        #region Properties

        /// <summary>
        /// PerfilId
        /// </summary>
        [ForeignKey(nameof(Perfil))]
        public Guid PerfilId { get; set; }

        /// <summary>
        /// Perfil
        /// </summary>
        [JsonIgnore]
        public virtual Perfil Perfil { get; set; }

        /// <summary>
        /// Modulo
        /// </summary>
        [ForeignKey(nameof(Modulo))]
        public Guid ModuloId { get; set; }

        /// <summary>
        /// Modulo
        /// </summary>
        [JsonIgnore]
        public virtual Modulo Modulo { get; set; }

        /// <summary>
        /// Permissões
        /// </summary>
        [JsonIgnore]
        public virtual List<PerfilModuloPermissao> Permissoes { get; set; } = new List<PerfilModuloPermissao>();
        
        #endregion

        #region Contructors

        public PerfilModulo()
        {

        }

        #endregion
    }
}
