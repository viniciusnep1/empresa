using core.events;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.pessoa_tipo.events
{
    public class PessoaTipoUpdatedEvent : EventLog
    {
        public PessoaTipoUpdatedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
