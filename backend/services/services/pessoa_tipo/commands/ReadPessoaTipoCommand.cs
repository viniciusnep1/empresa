using core.lib.extensions;
using core.seedwork;
using entities.empresa;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.pessoa_tipo.commands
{
    public class ReadPessoaTipoCommand : BasePaginateSpecification<PessoaTipo>, IRequest<Response>
    {
        public Guid FazendaId { get; set; }

        public string Parametro { get; set; }

        public override string Order { get; protected set; }

        public ReadPessoaTipoCommand(Guid fazendaid, string parametro)
        {
            Parametro = parametro;
            FazendaId = fazendaid;
            Build();
        }

        public override void Build()
        {
            Order = "Nome";
            //Criterias.Add(c => !c.Ativo);
            Criterias.Add(c => !c.DataExclusao.HasValue);
            if (!String.IsNullOrEmpty(Parametro))
            {
                Parametro = Parametro.RemoveAccent().ToLower();
                Criterias.Add(c => c.Nome.RemoveAccent().ToLower().Contains(Parametro) || c.Descricao.RemoveAccent().ToLower().Contains(Parametro));
            }
        }
    }
}
