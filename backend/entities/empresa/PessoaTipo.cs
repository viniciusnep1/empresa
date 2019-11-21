using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using core.seedwork;
using Newtonsoft.Json;

namespace entities.empresa
{
    [Table("pessoa_tipo", Schema = Schema.SCHEMA_EMPRESA)]
    public class PessoaTipo : EntidadeBase<Guid>
    {
        [StringLength(40)]
        [Required]
        public string Nome { get; set; }

        [StringLength(200)]
        public string Descricao { get; set; }

        public bool Operador { get; set; }

        public virtual ICollection<PessoaCategoriaPessoa> PessoaCategoriaPessoa { get; set; }


        #region Contructors

        public PessoaTipo()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Ativo = true;
        }

        #endregion Contructors
    }
}
