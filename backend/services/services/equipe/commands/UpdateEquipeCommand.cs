using services.services.equipe.viewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.equipe.commands
{
    public class UpdateEquipeCommand : EquipeCommand
    {
        public Guid Id { get; set; }
        public UpdateEquipeCommand(Guid id)
        {
            Id = id;
            DataAtualizacao = DateTime.Now;
            EquipePessoas = new List<EquipePessoaViewModel>();
        }
    }
}
