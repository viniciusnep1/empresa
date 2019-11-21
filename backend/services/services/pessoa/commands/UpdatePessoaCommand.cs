using core.commands;
using services.services.pessoa.viewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.pessoa.commands
{
    public class UpdatePessoaCommand : Command
    {
        public UpdatePessoaCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string Rg { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Nacionalidade { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public List<PessoaTipoViewModel> PessoasCategoriaId { get; set; }
    }
}
