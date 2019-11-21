using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace seguranca
{
    /// <summary>
    /// Papéis
    /// </summary>
    [Table("perfil", Schema = Schema.SCHEMA_SEGURANCA)]
    public partial class Perfil : IdentityRole<Guid>
    {
        #region Properties

        /// <summary>
        /// Nome
        /// </summary>
        [StringLength(40)]
        public override string Name
        {
            get => base.Name;
            set => base.Name = value;
        }

        /// <summary>
        /// Data Criação
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Data Criação
        /// </summary>
        public DateTime DataAlteracao { get; set; }

        /// <summary>
        /// Desativado
        /// </summary>
        public bool Desativado { get; set; }

        /// <summary>
        /// Papeis
        /// </summary>
        [JsonIgnore]
        public virtual List<UsuarioPerfil> Papeis { get; set; } = new List<UsuarioPerfil>();

        /// <summary>
        /// Modulos
        /// </summary>
        [JsonIgnore]
        public virtual List<PerfilModulo> Modulos { get; set; } = new List<PerfilModulo>();

        
        #endregion Properties

        #region Contructors

        public Perfil()
        {
            Id = Guid.NewGuid();
        }

        #endregion Contructors
    } 
} 
