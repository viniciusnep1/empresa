using core.seedwork;
using entities;
using entities.empresa;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.repositories
{
    public class PessoaCategoriaRepository : Repository<PessoaCategoriaPessoa, Guid>
    {
        public PessoaCategoriaRepository(EFApplicationContext context) : base(context)
        {

        }
    }
}
