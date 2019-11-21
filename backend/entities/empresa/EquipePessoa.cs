using core.seedwork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace entities.empresa
{
    [Table("equipe_pessoa", Schema = Schema.SCHEMA_EMPRESA)]
    public class EquipePessoa : EntidadeBase<Guid>
    {
        public EquipePessoa()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Ativo = true;
        }
        public Guid PessoaId { get; set; }
        public Guid EquipeId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual Equipe Equipe { get; set; }

    }
}
