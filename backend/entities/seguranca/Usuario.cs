using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using entities;
using core.seedwork;
using core.seedwork.interfaces;

namespace seguranca
{
    /// <summary>
    /// Usuário
    /// </summary>
    [Table("usuario", Schema = Schema.SCHEMA_SEGURANCA)]
    public partial class Usuario : IdentityUser<Guid>, IEntidadeBase<Guid>
    {
        #region Properties

        /// <summary>
        /// Nome
        /// </summary>
        [StringLength(40)]
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(40)]
        public override string Email
        {
            get => base.Email;
            set => base.Email = value;
        }

        /// <summary>
        /// Inscrição Federal
        /// </summary>
        [StringLength(14)]
        public string CpjCnpj { get; set; }

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

        #endregion

        #region Contructors

        public Usuario()
        {
            Id = Guid.NewGuid();
        }

        #endregion Contructors
    }
} 
