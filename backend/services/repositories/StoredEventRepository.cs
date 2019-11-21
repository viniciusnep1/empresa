using System;
using System.Collections.Generic;
using System.Text;
using entities;
using entities.logs;
using core.seedwork;

namespace services.repositories
{
    public class StoredEventRepository : Repository<StoredEvent, Guid>
    {
        public StoredEventRepository(EFApplicationContext context) : base(context)
        {

        }
    }
}
