using core.events;
using System;

namespace services.services.pessoa.events
{
    public class PessoaCreatedEvent : EventLog
    {
        public PessoaCreatedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
