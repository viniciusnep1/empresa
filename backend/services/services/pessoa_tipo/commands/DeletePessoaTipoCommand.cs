using core.commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.pessoa_tipo.commands
{
    public class DeletePessoaTipoCommand : Command
    {
        public DeletePessoaTipoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
