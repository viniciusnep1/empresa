using core.events;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.pessoa_tipo.events
{
    public class PessoaTipoCreatedEvent : EventLog
    {
        public PessoaTipoCreatedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
