using core.seedwork;
using entities.empresa;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.equipe.commands
{
    public class ReadEquipeCommand : BasePaginateSpecification<Equipe>, IRequest<Response>
    {
        public Guid FazendaId { get; set; }

        public override string Order { get; protected set; }

        public ReadEquipeCommand()
        {
            Build();
        }

        public override void Build()
        {
            Order = "Nome";
            Criterias.Add(x => x.Ativo);
            Criterias.Add(x => !x.DataExclusao.HasValue);
        }
    }
}
