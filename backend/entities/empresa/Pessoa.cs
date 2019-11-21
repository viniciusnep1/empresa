using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using core.seedwork;
using Newtonsoft.Json;
using seguranca;

namespace entities.empresa
{
    [Table("pessoa", Schema = Schema.SCHEMA_EMPRESA)]
    public class Pessoa : EntidadeBase<Guid>
    {
        [StringLength(40)]
        [Required]
        public string Nome { get; set; }

        [StringLength(40)]
        public string CpfCnpj { get; set; }

        [StringLength(11)]
        public string Rg { get; set; }

        public DateTime? DataNascimento { get; set; }

        [StringLength(40)]
        [Required]
        public string Sexo { get; set; }

        [StringLength(40)]
        public string Nacionalidade { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(12)]
        public string Telefone { get; set; }

        [JsonIgnore]
        public virtual ICollection<EquipePessoa> EquipePessoa { get; set; }

        public virtual ICollection<PessoaCategoriaPessoa> PessoaCategoriaPessoa { get; set; }

        public virtual Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }

        public Pessoa()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Ativo = true;
            PessoaCategoriaPessoa = new List<PessoaCategoriaPessoa>();
        }

    }
}
