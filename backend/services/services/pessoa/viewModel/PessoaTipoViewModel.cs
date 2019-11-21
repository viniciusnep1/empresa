using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.pessoa.viewModel
{
    public class PessoaTipoViewModel
    {
        public Guid Id { get; protected set; }
        public Guid PessoaTipoId { get; set; }
        public DateTime? DataAtualizacao { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataExclusao { get; protected set; }
        public bool Ativo { get; protected set; }

        public PessoaTipoViewModel()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Ativo = true;
        }
    }
}
