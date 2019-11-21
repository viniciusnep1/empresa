using core.commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.pessoa_tipo.commands
{
    public class UpdatePessoaTipoCommand : Command
    {
        public UpdatePessoaTipoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public bool Operador { get; set; }
    }
}
