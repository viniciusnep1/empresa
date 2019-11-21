using System;
using System.ComponentModel.DataAnnotations.Schema;
using core.seedwork;

namespace entities.logs
{
    [Table("stored_event", Schema = Schema.SCHEMA_LOGS)]
    public class StoredEvent : EntidadeBase<Guid>
    {
        public Guid AggregateId { get; set; }

        public string AggregateEntity { get; set; }

        public string Data { get; set; }

        public string Usuario { get; set; }

        public string MessageType { get; set; }

        public StoredEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}
