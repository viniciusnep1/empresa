using core.commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.equipe.commands
{
    public class DeleteEquipeCommand : Command
    {
        public Guid Id { get; set; }
        public DeleteEquipeCommand(Guid id)
        {
            Id = id;
        }
    }
}
