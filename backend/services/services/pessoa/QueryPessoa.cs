using core.bus;
using core.utils;
using entities.empresa;
using Microsoft.EntityFrameworkCore;
using services.repositories;
using services.services.pessoa.events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services.services.pessoa
{
    public class QueryPessoa
    {
        private readonly EquipePessoaRepository repository;
        private readonly PessoaCategoriaRepository categoriaRepository;
        private readonly PessoaRepository query;
        private readonly IMediatorHandler Bus;

        public QueryPessoa(EquipePessoaRepository repository, PessoaRepository query, IMediatorHandler Bus, PessoaCategoriaRepository categoriaRepository)
        {
            this.categoriaRepository = categoriaRepository;
            this.repository = repository;
            this.query = query;
            this.Bus = Bus;
        }

        public async Task<dynamic> GetPorId(Guid id)
        {
            var entidades = await query.GetAll().Select(x => new
            {
                x.Id,
                x.Nacionalidade,
                x.Nome,
                x.Rg,
                x.Sexo,
                x.Telefone,
                x.CpfCnpj,
                x.Email,
                DataNascimento = Convert.ToDateTime(x.DataNascimento.Value).ToString("MM/dd/yyyy"),
                Categorias = x.PessoaCategoriaPessoa.Select(h=> new {
                    h.Id,
                    h.PessoaId,
                    Label = h.PessoaTipo.Nome,
                    Value = h.PessoaTipoId
                }).Where(c=>c.PessoaId == id).ToList(),

            }).FirstOrDefaultAsync(x => x.Id == id);

            return entidades;
        }

        public async Task<Pessoa> AtivarPessoa(Guid id)
        {
            var entidade = query.Get(id);

            if (entidade == null) return entidade;

            entidade.Ativo = !entidade.Ativo;

            query.Update(entidade);
            if (await repository.CommitAsync())
            {
                await Bus.RaiseEvent(new PessoaUpdatedEvent(entidade.Id));
            }
            return entidade;
        }

        public async Task<List<dynamic>> GetPessoasPorEquipe(Guid equipeId)
        {
            return await repository.GetAll().Include(c => c.Pessoa).Include(c => c.Equipe)
                .Where(c => c.Equipe.Id.Equals(equipeId))
                .Select(c => new
                {
                    Id = c.PessoaId,
                    c.Pessoa.Nome,
                }).ToListAsync<dynamic>();
        }
    }
}
