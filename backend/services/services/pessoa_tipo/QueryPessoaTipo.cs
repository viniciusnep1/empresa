using entities.empresa;
using Microsoft.EntityFrameworkCore;
using services.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services.services.pessoa_tipo
{
    public class QueryPessoaTipo
    {
        private readonly PessoaTipoRepository repository;
        public QueryPessoaTipo(PessoaTipoRepository repository)
        {
            this.repository = repository;
        }

        public PessoaTipo GetPorId(Guid id)
        {
            return repository.Get(id);
        }

        public async Task<List<dynamic>> BuscarPorParametro(string busca)
        {
            return await repository.GetAll().Where(x => x.Nome.Contains(busca)).Select(c => new PessoaTipo
            {
                Nome = c.Nome,
                Descricao = c.Descricao,
                Id = c.Id
            }).ToListAsync<dynamic>(); 
        }
    }
}
