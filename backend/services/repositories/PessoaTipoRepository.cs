using core.seedwork;
using entities;
using entities.empresa;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.repositories
{
    public class PessoaTipoRepository : Repository<PessoaTipo, Guid>
    {
        public PessoaTipoRepository(EFApplicationContext context) : base(context)
        {

        }
    }
}
