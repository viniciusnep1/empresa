using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using entities;
using Newtonsoft.Json;
using core.seedwork;

namespace seguranca
{
    /// <summary>
    /// Modulo
    /// </summary>
    [Table("modulo_permissao", Schema = Schema.SCHEMA_SEGURANCA)]
    public class PerfilModuloPermissao : EntidadeBase
    {
        #region Properties

        /// <summary>
        /// ModuloId
        /// </summary>
        [ForeignKey(nameof(PerfilModulo))]
        public Guid PerfilModuloId { get; set; }

        /// <summary>
        /// Modulo
        /// </summary>
        [JsonIgnore]
        public virtual PerfilModulo PerfilModulo { get; set; }

        /// <summary>
        /// MenuItem
        /// </summary>
        [ForeignKey(nameof(Permissao))]
        public Guid PermissaoId { get; set; }

        /// <summary>
        /// MenuItem
        /// </summary>
        [JsonIgnore]
        public virtual Permissao Permissao { get; set; }

        #endregion

        #region Contructors

        public PerfilModuloPermissao()
        {

        }

        #endregion
    }
}
