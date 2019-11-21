using core.bus;
using core.seedwork;
using core.utils;
using entities.empresa;
using MediatR;
using Microsoft.EntityFrameworkCore;
using services.gateways.repositories;
using services.repositories;
using services.services.pessoa.commands;
using services.services.pessoa.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace services.services.pessoa
{
    public class HandlerPessoa : CommandHandler,
        IRequestHandler<ReadPessoaCommand, Response>,
        IRequestHandler<CreatePessoaCommand, Response>,
        IRequestHandler<UpdatePessoaCommand, Response>,
        IRequestHandler<DeletePessoaCommand, Response>

    {
        private readonly PessoaRepository repository;
        private readonly PessoaCategoriaRepository pessoaCategoriaRepository;
        private readonly IMediatorHandler Bus;
        public HandlerPessoa(PessoaRepository repository, IMediatorHandler bus, PessoaCategoriaRepository pessoaCategoriaRepository)
        {
            Bus = bus;
            this.pessoaCategoriaRepository = pessoaCategoriaRepository;
            this.repository = repository;

        }
        public async Task<Response> Handle(ReadPessoaCommand message, CancellationToken cancellationToken)
        {
            var paginate = repository.Paginate(message);

            var view = new ViewModelBase
            {
                Items = paginate.items.Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.CpfCnpj,
                    CategoriaPessoa = x.PessoaCategoriaPessoa.Select(c => new
                    {
                        c.PessoaTipo.Nome
                    }),
                    x.Ativo
                }).OrderBy(c => c.Nome),
                Page = paginate.pageCount,
                Total = paginate.total
            };

            return await Task.FromResult(new Response(view));
        }

        public async Task<Response> Handle(CreatePessoaCommand message, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                var existePessoa = await repository.GetAll().AnyAsync(x=>x.CpfCnpj.Equals(message.CpfCnpj));

                if (existePessoa)
                    return await Task.FromResult(new Response(new Exception ("Esse CPF/CNPJ já consta no banco de dados!")));

                var entidade = new Pessoa
                {
                    Nome = message.Nome,
                    CpfCnpj = message.CpfCnpj,
                    DataNascimento = message.DataNascimento,
                    Email = message.Email,
                    Nacionalidade = message.Nacionalidade,
                    Rg = message.Rg,
                    Sexo = message.Sexo,
                    Telefone = message.Telefone,
                    UsuarioId = Guid.Parse("762238c2-7e4e-4033-a59e-ef0eb042b4fa")
                };

                message.PessoasCategoriaId.ForEach(categoriaPessoa =>
                {
                    entidade.PessoaCategoriaPessoa.Add(new PessoaCategoriaPessoa
                    {
                        PessoaId = entidade.Id,
                        PessoaTipoId = categoriaPessoa.PessoaTipoId,
                    });
                });

                await repository.CreateAsync(entidade);

                await repository.CommitAsync();
                

                return await Task.FromResult(new Response());
            });
        }

        public async Task<Response> Handle(UpdatePessoaCommand message, CancellationToken cancellationToken)
        {
            var entidade = repository.Get(message.Id);
            if (entidade == null) return await Task.FromResult(new Response("Entidade não encontrada."));

            entidade.Nome = message.Nome;
            entidade.Nacionalidade = message.Nacionalidade;
            entidade.Rg = message.Rg;
            entidade.Sexo = message.Sexo;
            entidade.CpfCnpj = message.CpfCnpj;
            entidade.DataNascimento = message.DataNascimento;
            entidade.Email = message.Email;
            entidade.Telefone = message.Telefone;
            entidade.DataAtualizacao = DateTime.Now;

            pessoaCategoriaRepository.DeleteRange(x => x.PessoaId.Equals(message.Id));

            message.PessoasCategoriaId.ForEach(categoriaPessoa =>
            {
                entidade.PessoaCategoriaPessoa.Add(new PessoaCategoriaPessoa
                {
                    PessoaId = entidade.Id,
                    PessoaTipoId = categoriaPessoa.PessoaTipoId,
                    Ativo = categoriaPessoa.Ativo,
                    DataAtualizacao = DateTime.Now,
                });
            });

            repository.Update(entidade);

            if (await repository.CommitAsync())
            {
                await Bus.RaiseEvent(new PessoaUpdatedEvent(entidade.Id));
            }

            return await Task.FromResult(new Response());
        }

        public async Task<Response> Handle(DeletePessoaCommand message, CancellationToken cancellationToken)
        {
            var entidade = repository.Get(message.Id);
            if (entidade == null) return await Task.FromResult(new Response("Entidade não encontrada."));

            entidade.DataExclusao = DateTime.Now;

            pessoaCategoriaRepository.DeleteRange(x => x.PessoaId.Equals(message.Id));

            repository.Update(entidade);

            if (await repository.CommitAsync())
            {
                await Bus.RaiseEvent(new PessoaDeletedEvent(message.Id));
            }

            return await Task.FromResult(new Response());
        }


        public void Dispose()
        {
            repository.Dispose();
        }


    }
}
