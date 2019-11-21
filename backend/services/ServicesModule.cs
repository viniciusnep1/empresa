using Autofac;
using services.gateways.repositories;
using hateoas.infrastructure;
using services.repositories;
using core.events;
using MediatR;
using core.seedwork;
using core.bus;
using services.services.equipe;
using services.services.pessoa;
using services.services.pessoa.commands;
using services.services.pessoa_tipo;
using services.services.pessoa_tipo.commands;
using services.services.pessoa.events;
using services.services.equipe.commands;

namespace services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            // Infra
            containerBuilder.RegisterType<InMemoryBus>().As<IMediatorHandler>();
            containerBuilder.RegisterType<HateoasOptionsService>().As<IHateoasOptionsService>();

            // Repositories
            containerBuilder.RegisterType<EquipeRepository>();
            containerBuilder.RegisterType<EquipePessoaRepository>();
            containerBuilder.RegisterType<StoredEventRepository>();
            containerBuilder.RegisterType<EventStoreRepository>().As<IEventStore>();
            containerBuilder.RegisterType<PessoaRepository>();
            containerBuilder.RegisterType<PessoaTipoRepository>();
            containerBuilder.RegisterType<PessoaCategoriaRepository>();

            // Queries
            containerBuilder.RegisterType<QueryEquipe>();
            containerBuilder.RegisterType<QueryPessoaTipo>();
            containerBuilder.RegisterType<QueryPessoa>();

            // Pessoa Commands
            containerBuilder.RegisterType<HandlerPessoa>().As<IRequestHandler<CreatePessoaCommand, Response>>();

            //Pessoa Tipo Commands
            containerBuilder.RegisterType<HandlerPessoaTipo>().As<IRequestHandler<CreatePessoaTipoCommand, Response>>();

            //Equipe Commands
            containerBuilder.RegisterType<HandleEquipes>().As<IRequestHandler<ReadEquipeCommand, Response>>();
        }
    }
}
