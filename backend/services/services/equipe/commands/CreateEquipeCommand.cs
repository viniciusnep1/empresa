using services.services.equipe.viewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.equipe.commands
{
    public class CreateEquipeCommand : EquipeCommand
    {
        public CreateEquipeCommand()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Ativo = true;
            EquipePessoas = new List<EquipePessoaViewModel>();
        }
    }
}
