using core.seedwork;
using entities.empresa;
using MediatR;
using System;
using System.Collections.Generic;
using core.lib.extensions;
using System.Globalization;
using System.Linq;
using System.Text;
using usecases.converters;

namespace services.services.pessoa.commands
{
    public class ReadPessoaCommand : BasePaginateSpecification<Pessoa>, IRequest<Response>
    {
        public string Parametro { get; set; }
        public override string Order { get; protected set; }

        public ReadPessoaCommand( string parametro)
        {
            Parametro = parametro;
            Build();
        }


        public override void Build()
        {
            Order = "Nome";
            Criterias.Add(c => !c.DataExclusao.HasValue);
            if (!String.IsNullOrEmpty(Parametro))
            {
                Parametro = Parametro.RemoveAccent().ToLower();
                AddInclude(c => c.PessoaCategoriaPessoa);

                Criterias.Add(
                    c =>
                    c.Nome.RemoveAccent().ToLower().Contains(Parametro) ||
                    c.CpfCnpj.RemoveAccent().ToLower().Contains(Parametro) ||
                    c.PessoaCategoriaPessoa.Any(x=> x.PessoaTipo.Nome.RemoveAccent().ToLower().Contains(Parametro))
                );
            }
        }

    }
}
