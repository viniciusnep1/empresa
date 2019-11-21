using core.seedwork;
using entities;
using entities.empresa;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace services.repositories
{
    public class PessoaRepository : Repository<Pessoa, Guid>
    {
        public PessoaRepository(EFApplicationContext context) : base(context)
        {

        }
    }
}
