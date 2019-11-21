using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using core.seedwork;
using Newtonsoft.Json;

namespace entities.empresa
{
 
    [Table("equipe", Schema = Schema.SCHEMA_EMPRESA)]
    public class Equipe : EntidadeBase<Guid>
    {
      
        [StringLength(40)]
        [Required]
        public string Nome { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        public virtual ICollection<EquipePessoa> EquipePessoa { get; set; }


        #region Contructors

        public Equipe()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Ativo = true;
            EquipePessoa = new List<EquipePessoa>();
        }

        #endregion Contructors
    }
}
