using core.events;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.pessoa_tipo.events
{
    public class PessoaTipoDeletedEvent : EventLog
    {
        public PessoaTipoDeletedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
