using core.bus;
using core.seedwork;
using core.utils;
using entities.empresa;
using MediatR;
using Microsoft.EntityFrameworkCore;
using services.repositories;
using services.services.pessoa_tipo.commands;
using services.services.pessoa_tipo.events;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace services.services.pessoa_tipo
{
    public class HandlerPessoaTipo : CommandHandler,
        IRequestHandler<ReadPessoaTipoCommand, Response>,
        IRequestHandler<CreatePessoaTipoCommand, Response>,
        IRequestHandler<UpdatePessoaTipoCommand, Response>,
        IRequestHandler<DeletePessoaTipoCommand, Response>
    {
        private readonly PessoaTipoRepository repository;
        private readonly IMediatorHandler Bus;
        public HandlerPessoaTipo(PessoaTipoRepository repository, IMediatorHandler bus)
        {
            Bus = bus;
            this.repository = repository;
        }

        public async Task<Response> Handle(ReadPessoaTipoCommand message, CancellationToken cancellationToken)
        {
            var paginate = repository.Paginate(message);

            var view = new ViewModelBase
            {
                Items = paginate.items.Select(c => new {
                    c.Id,
                    c.Nome,
                    c.Descricao,
                    c.Ativo,
                    c.Operador
                }),
                Page = paginate.pageCount,
                Total = paginate.total
            };

            return await Task.FromResult(new Response(view));
        }

        public async Task<Response> Handle(CreatePessoaTipoCommand message, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                var entidade = new PessoaTipo
                {
                    Nome = message.Nome,
                    Id = message.Id,
                    Descricao = message.Descricao,
                    Operador = message.Operador
                    
                };

                await repository.CreateAsync(entidade);

                if (await repository.CommitAsync())
                {
                    await Bus.RaiseEvent(new PessoaTipoCreatedEvent(message.Id));
                }

                return await Task.FromResult(new Response());
            });
        }

        public async Task<Response> Handle(DeletePessoaTipoCommand message, CancellationToken cancellationToken)
        {
            var entidade = repository.Get(message.Id);
            if (entidade == null) return await Task.FromResult(new Response("Entidade não encontrada."));

            entidade.DataExclusao = DateTime.Now;

            repository.Update(entidade);

            if (await repository.CommitAsync())
            {
                await Bus.RaiseEvent(new PessoaTipoDeletedEvent(message.Id));
            }

            return await Task.FromResult(new Response());
        }

        public async Task<Response> Handle(UpdatePessoaTipoCommand message, CancellationToken cancellationToken)
        {
            var entidade = repository.Get(message.Id);
            if (entidade == null) return await Task.FromResult(new Response("Entidade não encontrada."));

            entidade.Nome = message.Nome;
            entidade.Descricao = message.Descricao;
            entidade.Operador = message.Operador;

            repository.Update(entidade);

            if (await repository.CommitAsync())
            {
                await Bus.RaiseEvent(new PessoaTipoUpdatedEvent(entidade.Id));
            }

            return await Task.FromResult(new Response());
        }

        public void Dispose()
        {
            repository.Dispose();
        }
    }
}
