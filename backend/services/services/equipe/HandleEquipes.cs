using core.bus;
using core.seedwork;
using core.utils;
using entities.empresa;
using MediatR;
using Microsoft.EntityFrameworkCore;
using services.gateways.repositories;
using services.repositories;
using services.services.equipe.commands;
using services.services.equipe.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace services.services.equipe
{
    public class HandleEquipes : CommandHandler,
        IRequestHandler<ReadEquipeCommand, Response>,
        IRequestHandler<CreateEquipeCommand, Response>,
        IRequestHandler<UpdateEquipeCommand, Response>,
        IRequestHandler<DeleteEquipeCommand, Response>
    {
        private readonly EquipeRepository repository;
        private readonly EquipePessoaRepository equipePessoaRepository;
        private readonly IMediatorHandler Bus;

        public HandleEquipes(EquipeRepository repository, IMediatorHandler Bus, EquipePessoaRepository equipePessoaRepository)
        {
            this.repository = repository;
            this.Bus = Bus;
            this.equipePessoaRepository = equipePessoaRepository;
        }

        public async Task<Response> Handle(ReadEquipeCommand message, CancellationToken cancellationToken)
        {
            var paginate = repository.Paginate(message);

            var apontamentoViewModel = new ViewModelBase
            {
                Items = paginate.items.OrderBy(c => c.Nome).Select(c => new {
                    c.Id,
                    c.Nome,
                    c.Descricao
                }),
                Page = paginate.pageCount,
                Total = paginate.total
            };

            return await Task.FromResult(new Response(apontamentoViewModel));
        }

        public async Task<Response> Handle(CreateEquipeCommand message, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () => {

                var entidade = new Equipe
                {
                    Id = message.Id,
                    DataAtualizacao = null,
                    DataExclusao = null,
                    DataCriacao = DateTime.Now,
                    Nome = message.Nome,
                    Descricao = message.Descricao
                };

                message.EquipePessoas.ForEach(equipePessoa =>
                {
                    entidade.EquipePessoa.Add(new EquipePessoa
                    {
                        Id = equipePessoa.Id,
                        EquipeId = entidade.Id,
                        PessoaId = equipePessoa.PessoaId,
                        Ativo = equipePessoa.Ativo,
                        DataAtualizacao = equipePessoa.DataAtualizacao,
                        DataCriacao = equipePessoa.DataCriacao,
                        DataExclusao = equipePessoa.DataExclusao
                    });
                });

                await repository.CreateAsync(entidade);

                if (await repository.CommitAsync())
                {
                    await Bus.RaiseEvent(new EquipeEvent(message.Id));
                }

                return await Task.FromResult(new Response());

            });
        }

        public async Task<Response> Handle(UpdateEquipeCommand message, CancellationToken cancellationToken)
        {
            var entidade = repository.Get(message.Id);

            entidade.DataAtualizacao = DateTime.Now;
            entidade.DataExclusao = null;
            entidade.Nome = message.Nome;
            entidade.Descricao = message.Descricao;

            equipePessoaRepository.DeleteRange(x=>x.EquipeId.Equals(message.Id));

            message.EquipePessoas.ForEach(equipePessoa =>
            {
                entidade.EquipePessoa.Add(new EquipePessoa
                {
                    Id = equipePessoa.Id,
                    EquipeId = entidade.Id,
                    PessoaId = equipePessoa.PessoaId,
                    Ativo = equipePessoa.Ativo,
                    DataAtualizacao = equipePessoa.DataAtualizacao,
                    DataCriacao = equipePessoa.DataCriacao,
                    DataExclusao = equipePessoa.DataExclusao,
                });
            });

            if (entidade == null) await Task.FromResult(new Response("Entidade não encontrada."));

            repository.Update(entidade);


            if (await repository.CommitAsync())
            {
                await Bus.RaiseEvent(new EquipeEvent(message.Id));
            }

            return await Task.FromResult(new Response());
        }

        public async Task<Response> Handle(DeleteEquipeCommand message, CancellationToken cancellationToken)
        {
            var entidade = repository.Get(message.Id);

            entidade.Ativo = false;
            entidade.DataExclusao = DateTime.Now;

            if (entidade == null) await Task.FromResult(new Response("Entidade não encontrada."));

            repository.Update(entidade);
            equipePessoaRepository.DeleteRange(x => x.EquipeId.Equals(message.Id));


            if (await repository.CommitAsync())
            {
                await Bus.RaiseEvent(new EquipeEvent(message.Id));
            }

            return await Task.FromResult(new Response());
        }

        public void Dispose()
        {
            repository.Dispose();
        }
    }
}
