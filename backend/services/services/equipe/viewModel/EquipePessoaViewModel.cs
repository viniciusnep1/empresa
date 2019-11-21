using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.equipe.viewModel
{
    public class EquipePessoaViewModel
    {
        public Guid Id { get; protected set; }
        public Guid PessoaId { get; set; }
        public DateTime? DataAtualizacao { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataExclusao { get; protected set; }
        public bool Ativo { get; protected set; }

        public EquipePessoaViewModel()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Ativo = true;
        }
    }
}
