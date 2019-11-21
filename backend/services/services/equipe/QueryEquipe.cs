using services.gateways.repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using services.repositories;
using entities.empresa;

namespace services.services.equipe
{
    public class QueryEquipe
    {
        private readonly EquipeRepository repository;
        private readonly EquipePessoaRepository equipePessoaRepository;

        public QueryEquipe(EquipeRepository repository, EquipePessoaRepository equipePessoaRepository)
        {
            this.repository = repository;
            this.equipePessoaRepository = equipePessoaRepository;
        }

        public async Task<List<dynamic>> GetEquipes()
        {
            return await repository.GetAll()
                .Where(c => c.Ativo && !c.DataExclusao.HasValue)
                .Select(c => new
                {
                    c.Id,
                    c.Nome,
                }).ToListAsync<dynamic>();
        }

        public Equipe GetById(Guid id)
        {
            return repository.Get(id);
        }

        public async Task<List<dynamic>> GetPessoasPorEquipe(Guid equipeId)
        {
            return await equipePessoaRepository.GetAll()
                .Where(c => c.EquipeId == equipeId)
                .Select(c => new
                {
                     Id = c.PessoaId,
                     c.Pessoa.Nome,
                }).ToListAsync<dynamic>();
        }


    }
}
