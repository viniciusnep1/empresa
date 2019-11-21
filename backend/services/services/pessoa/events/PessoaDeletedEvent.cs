using core.events;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.pessoa.events
{
    public class PessoaDeletedEvent : EventLog
    {
        public PessoaDeletedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
