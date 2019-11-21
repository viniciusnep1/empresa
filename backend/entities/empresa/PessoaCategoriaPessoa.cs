using core.seedwork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace entities.empresa
{
    [Table("pessoa_categoria_pessoa", Schema = Schema.SCHEMA_EMPRESA)]
    public class PessoaCategoriaPessoa : EntidadeBase<Guid>
    {
        public PessoaCategoriaPessoa()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Ativo = true;
        }
        public Guid PessoaTipoId { get; set; }
        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual PessoaTipo PessoaTipo { get; set; }

    }
}
