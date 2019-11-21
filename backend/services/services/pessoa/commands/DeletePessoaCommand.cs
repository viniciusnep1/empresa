using core.commands;
using System;

namespace services.services.pessoa.commands
{
    public class DeletePessoaCommand : Command
    {
        public DeletePessoaCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }

    }
}
