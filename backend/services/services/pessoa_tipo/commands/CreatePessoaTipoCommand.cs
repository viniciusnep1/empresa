using core.commands;
using System;

namespace services.services.pessoa_tipo.commands
{
    public class CreatePessoaTipoCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Nome { get; set; }
        public Guid FazendaId { get; set; }
        public string Descricao { get; set; }
        public bool Operador { get; set; }

        public CreatePessoaTipoCommand()
        {
            Id = Guid.NewGuid();
        }

    }
}
