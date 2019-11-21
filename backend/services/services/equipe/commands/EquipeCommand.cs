using core.commands;
using services.services.equipe.viewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.equipe.commands
{
    public class EquipeCommand : Command
    {
        public bool Ativo { get; protected set; }
        public DateTime? DataAtualizacao { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataExclusao { get; protected set; }
        public Guid FazendaId { get; set; }
        public Guid Id { get; protected set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<EquipePessoaViewModel> EquipePessoas { get; set; }
    }
}
