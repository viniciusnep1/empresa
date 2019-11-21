using core.seedwork;
using entities;
using entities.empresa;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.repositories
{
    public class EquipePessoaRepository : Repository<EquipePessoa, Guid>
    {
        public EquipePessoaRepository(EFApplicationContext context) : base(context)
        {
        }
    }
}
